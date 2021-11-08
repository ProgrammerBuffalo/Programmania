using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Programmania.ViewModels;
using Programmania.Services;
using Programmania.Models;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Programmania.Attributes;
using Programmania.Services.Interfaces;

namespace Programmania.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private IAccountService accountService;
        private IXMLService xmlService;
        private IFileService fileService;

        public AccountController(DAL.ProgrammaniaDBContext context, IAccountService accService,
            IXMLService xmlService, IFileService fileService)
        {
            this.dbContext = context;
            this.accountService = accService;
            this.xmlService = xmlService;
            this.fileService = fileService;
        }

        [Route("refresh-token")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult RefreshTokensAuthentication()
        {
            var rtCookie = Request.Cookies["RefreshToken"];

            if (accountService.RefreshTokens(rtCookie, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                out string generatedJWT, out string generatedRToken, out User user))
            {
                return Unauthorized("Invalid refresh token");
            }

            setCookieTokens(generatedRToken, generatedJWT);

            return Ok();
        }

        [Route("revoke-token")]
        [HttpPost]
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
        [AllowAnonymous]
        public async Task<IActionResult> MakeAuthorization(AuthenticationRequestVM authenticationRequest)
        {
            if (!ModelState.IsValid)
            {
                Models.User user = dbContext.Users.FirstOrDefault(u => u.Login == authenticationRequest.Email && u.Password == authenticationRequest.Password);
                if (user == null)
                {
                    string json = Utilities.FormError.MakeServerError("Error", "email or password was entered wrong");
                    return BadRequest(json);
                }
                else
                {
                    var response = await accountService.Authenticate(authenticationRequest, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

                    setCookieTokens(response.RefreshToken, response.JWTToken);

                    return Ok(response);
                }
            }
            else
            {
                string json = Utilities.FormError.MakeModelError(ModelState);
                return BadRequest(json);
            }
        }

        [Route("registration")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> MakeRegistration(RegistrationVM registrationVM)
        {
            if (!ModelState.IsValid)
            {
                User user = dbContext.Users.FirstOrDefault(u => u.Login == registrationVM.Email || u.Name == registrationVM.Nickname);
                if (user == null)
                {
                    AuthenticationResponseVM response;
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        user = new User()
                        {
                            Name = registrationVM.Nickname,
                            Password = registrationVM.Password,
                            Login = registrationVM.Email
                        };

                        if (registrationVM.FormFile != null)
                        {
                            SqlFileContext sqlFileContext = fileService.AddEmptyDocument(user.Name + Path.GetExtension(registrationVM.FormFile.FileName));
                            fileService.FillDocumentContent(sqlFileContext, registrationVM.FormFile);
                            user.ImageId = sqlFileContext.StreamId;
                        }

                        dbContext.Users.Add(user);

                        dbContext.SaveChanges();
                        response = await accountService.Authenticate(new AuthenticationRequestVM { Email = user.Login, Password = user.Password },
                            HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

                        transaction.Commit();
                    }
                    setCookieTokens(response.RefreshToken, response.JWTToken);

                    return Ok(response);
                }
                else
                {
                    string jsonResponse;
                    if (user.Login == registrationVM.Email)
                        jsonResponse = Utilities.FormError.MakeServerError("Error", "The given email already exists");
                    else
                        jsonResponse = Utilities.FormError.MakeServerError("Error", "The given password already exists");
                    return BadRequest(jsonResponse);
                }
            }
            else
            {
                string json = Utilities.FormError.MakeModelError(ModelState);
                return BadRequest(json);
            }
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
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    HttpOnly = true
                });
        }

    }
}