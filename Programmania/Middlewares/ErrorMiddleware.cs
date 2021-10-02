using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Programmania.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Programmania.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate next;
        private IWebHostEnvironment hostingEnvironment;
        private IActionDescriptorCollectionProvider actionDescriptorCollection;

        public ErrorMiddleware(RequestDelegate next,
            IWebHostEnvironment hostingEnvironment, IActionDescriptorCollectionProvider actionDescriptorCollection)
        {
            this.actionDescriptorCollection = actionDescriptorCollection;
            this.hostingEnvironment = hostingEnvironment;
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            var routes = actionDescriptorCollection.ActionDescriptors.Items.Select(a => "/" + a.AttributeRouteInfo.Template).ToList();

            try
            {
                if (!routes.Any(route => path.Equals(route, StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new PageNotFoundException();
                }
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                switch (ex)
                {
                    case PageNotFoundException e:
                        {
                            response.StatusCode = StatusCodes.Status404NotFound;
                            break;
                        }
                    case AuthorizeException e:
                        {
                            response.StatusCode = StatusCodes.Status401Unauthorized;
                            response.Headers.Add("RedirectTo", path.Value);
                            break;
                        }
                    default:
                        {
                            response.StatusCode = StatusCodes.Status500InternalServerError;
                            break;
                        }

                }
                var page = await File.ReadAllTextAsync(Path.Combine(hostingEnvironment.WebRootPath + "/statusPages/html",
                    response.StatusCode.ToString() + ".html"));
                await response.WriteAsync(page);
            }
        }
    }
}
