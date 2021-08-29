using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Programmania.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Programmania.Services;
using Programmania.Models;

namespace Programmania.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private IAccountService accountService;

        public AccountController(DAL.ProgrammaniaDBContext context, IAccountService accService)
        {
            this.dbContext = context;
            this.accountService = accService;
        }

        [Route("refresh-token")]
        [HttpPost]
        public async Task<IActionResult> RefreshTokensAuthentication()
        {
            var rtCookie = Request.Cookies["RefreshToken"];

            AuthenticationResponseVM authenticationResponse = await accountService.RefreshTokens(rtCookie, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            if (authenticationResponse == null)
                return Unauthorized("Invalid refresh token");

            setCookieTokens(authenticationResponse.RefreshToken, authenticationResponse.JWTToken);

            return Ok(authenticationResponse);
        }

        [Route("revoke-token")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RevokeToken()
        {
            var rtCookie = Request.Cookies["RefreshToken"];

            if (string.IsNullOrEmpty(rtCookie))
                return BadRequest(new { message = "Refresh token is empty" });

            if (!await accountService.RevokeToken(rtCookie, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString()))
                return NotFound(new { message = "Refresh token is null" });

            return Ok("Token revorked");

        }

        [Route("authorization")]
        [HttpPost]
        public async Task<IActionResult> MakeAuthorization(AuthenticationRequestVM authenticationRequest)
        {
            if (ModelState.IsValid)
            {
                Models.User user = dbContext.Users.FirstOrDefault(u => u.Login == authenticationRequest.Email && u.Password == authenticationRequest.Password);
                if (user == null)
                    ModelState.AddModelError("AuthorizationModelError", "Login or password is not correct");
                else
                {
                    var response = await accountService.Authenticate(authenticationRequest, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

                    setCookieTokens(response.RefreshToken, response.JWTToken);

                    return Ok(response);
                }
            }
            return BadRequest(authenticationRequest);
        }

        [Route("registration")]
        [HttpPost]
        public async Task<IActionResult> MakeRegistration(RegistrationVM registrationVM)
        {
            if (ModelState.IsValid)
            {
                User user = dbContext.Users.FirstOrDefault(u => u.Login == registrationVM.Email || u.Name == registrationVM.Nickname);
                if (user == null)
                {
                    AuthenticationResponseVM response =
                        await accountService.Registrate(registrationVM, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

                    setCookieTokens(response.RefreshToken, response.JWTToken);

                    return Ok(response);
                }
                else
                {
                    if (user.Name == registrationVM.Nickname)
                        ModelState.AddModelError("NicknameModelError", "User with this nickname already exists");
                    else
                        ModelState.AddModelError("EmailModelError", "User with this email already exists");
                }
            }
            return BadRequest(registrationVM);
        }

        private void setCookieTokens(string rtoken, string jwttoken)
        {
            Response.Cookies.Append("RefreshToken", rtoken,
                new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(1),
                    HttpOnly = true
                });

            Response.Cookies.Append("JwtToken", jwttoken,
                new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    HttpOnly = true
                });
        }

    }
}