using Microsoft.AspNetCore.Mvc;
using Programmania.Attributes;
using Programmania.DAL;
using Programmania.Models;
using Programmania.Services.Interfaces;
using Programmania.ViewModels;
using Programmania.ViewModels.Validators;
using System.Linq;

namespace Programmania.Controllers
{
    [Route("Profile")]
    [Authorize]
    public class ProfileController : Controller
    {
        private ProgrammaniaDBContext dBContext;
        private IFileService fileService;

        public ProfileController(ProgrammaniaDBContext dBContext, IFileService fileService)
        {
            this.dBContext = dBContext;
            this.fileService = fileService;
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpGet("")]
        public IActionResult Profile()
        {
            //var user = HttpContext.Items["User"] as Models.User;
            //UserProfileVM profileVM = new UserProfileVM(true)
            //{
            //    Nickname = user.Name,
            //    Avatar = fileService.GetSqlFileContext(user.ImageId)?.TransactionContext,
            //    Email = user.Login,
            //    Expierence = user.Exp,
            //};
            return View("/Views/Home/Profile.cshtml", new UserProfileVM(true));
        }

        [AllowAnonymous]
        [HttpGet("show")]
        public IActionResult Profile(string userIdCode)
        {
            int userId = int.Parse(Utilities.Encryptor.DecryptString(userIdCode));
            //var profileVM = dBContext.Users.Where(u => u.Id == userId)
            //    .Select(p => new UserProfileVM(false)
            //    {
            //        Nickname = p.Name,
            //        Avatar = fileService.GetSqlFileContext()?.TransactionContext,
            //        Expierence = p.Exp,
            //    });

            //var a = HttpContext.Items["User"] as Models.User;
            return View("/Views/Home/Profile.cshtml", null);
        }

        //
        [AllowAnonymous]
        [HttpPost("change-nickname")]
        public IActionResult ChangeNickname(NicknameValidator inputs)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.Items["User"] as User;
                var dbUser = dBContext.Users.FirstOrDefault(u => u.Id == user.Id);
                if (dbUser != null)
                {
                    if (user.Login == dbUser.Login)
                    {
                        dbUser.Name = inputs.Nickname;
                        dBContext.SaveChanges();
                        return Ok();
                    }
                    return BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                string json = Utilities.FormError.MakeModelError(ModelState);
                return BadRequest(json);
            }
        }

        //
        [AllowAnonymous]
        [HttpPost("change-avatar")]
        public IActionResult ChangeNickname(FileValidator inputs)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.Items["User"] as Models.User;
                var dbUser = dBContext.Users.FirstOrDefault(u => u.Id == user.Id);
                if (dbUser != null)
                {
                    if (user.Login == dbUser.Login)
                    {
                        fileService.UpdateDocument(user.ImageId, inputs.File);
                        dBContext.SaveChanges();
                        return Ok();
                    }
                    return BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                string json = Utilities.FormError.MakeModelError(ModelState);
                return BadRequest(json);
            }
        }

        [AllowAnonymous]
        [HttpPost("change-password")]
        public IActionResult ChangePassword(ChangePasswordValidator validator)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.Items["User"] as User;
                if (user.Password == validator.OldPassword)
                {
                    var password = dBContext.Users.FirstOrDefault(u => u.Password == validator.NewPassword);
                    if (password == null)
                    {
                        user.Password = validator.NewPassword;
                        dBContext.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        //
        [AllowAnonymous]
        [HttpGet("get-games")]
        public IActionResult GetGames()
        {
            return Json(null);
        }

        //
        [AllowAnonymous]
        [HttpGet("get-achivments")]
        public IActionResult GetAchivments()
        {
            return Json(null);
        }

        //
        [AllowAnonymous]
        [HttpGet("get-user-info")]
        public IActionResult GetUserInfo()
        {
            return Json(null);
        }
    }
}

//[Route("")]
//[AllowAnonymous]
//public IActionResult Profile(int userId)
//{
//    var a = HttpContext.Items["User"] as Models.User;
//    ViewModels.UserProfileVM profileVM = new ViewModels.UserProfileVM(true);
//    return View("/Views/Home/Profile.cshtml", profileVM);
//}
