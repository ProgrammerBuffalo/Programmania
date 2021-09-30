using Microsoft.AspNetCore.Http;
using Programmania.Attributes;
using Programmania.Models;
using Programmania.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Middlewares
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate next;

        public JWTMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, IAccountService accountService)
        {
            var endpoint = httpContext.GetEndpoint();
            var isAllowAnonymous = !endpoint?.Metadata.Any(x => x.GetType() == typeof(AllowAnonymousAttribute));

            if (!isAllowAnonymous.GetValueOrDefault())
            {
                await next(httpContext);
                return;
            }

            bool isValidated = false;

            var jwtToken = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last() 
                ?? httpContext.Request.Cookies["JwtToken"];

            if (jwtToken != null)
            {
                if (isValidated = accountService.ValidateJWTToken(jwtToken, out int? userId, out string login))
                {
                    var user = accountService.GetUserDBId(userId, login);
                    if (userId != null)
                    {
                        httpContext.Items["User"] = user;
                    }
                    else
                    {
                        throw new Exceptions.AuthorizeException("User doesn't exist");
                    }
                }
            }
            else if(!isValidated && jwtToken == null)
            {
                if (accountService.RefreshTokens(httpContext.Request.Cookies["RefreshToken"], "AZ", out string generatedJwtToken, out string generatedRToken, out User user))
                {
                    accountService.SetCookiesInApp(httpContext.Response, generatedJwtToken, generatedRToken);
                    httpContext.Items["User"] = user;
                }
                else
                {
                    throw new Exceptions.AuthorizeException("Token doesn't exist or is not valid");
                }
            }

            await next(httpContext);
        }
    }
}