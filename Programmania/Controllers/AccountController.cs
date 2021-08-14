using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Programmania.ViewModels;

namespace Programmania.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Route("Account/Registration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeRegistration(RegistrationVM registrationVM)
        {
            if (ModelState.IsValid)
            {
                using (DAL.ProgrammaniaDBContext dbContext = new DAL.ProgrammaniaDBContext())
                {
                    Models.User user = dbContext.Users.FirstOrDefault(u => u.Login == registrationVM.Email);
                    if (user == null)
                    {
                        dbContext.Users.Add(new Models.User()
                        {
                            Name = registrationVM.Nickname,
                            Password = registrationVM.Password,
                            Login = registrationVM.Email
                        });

                        await dbContext.SaveChangesAsync();

                        await Authenticate(registrationVM.Email);

                        return RedirectToAction("Index", "Main");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User with this login/nickname already exists");
                    }
                }
            }
            return View(registrationVM);
        }

        private async Task Authenticate(string email)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AppCookie", ClaimsIdentity.DefaultRoleClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}