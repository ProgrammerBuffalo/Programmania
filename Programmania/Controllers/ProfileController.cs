using Microsoft.AspNetCore.Mvc;
using Programmania.Attributes;
using Programmania.DAL;
using Programmania.Models;
using Programmania.Services.Interfaces;
using Programmania.ViewModels;
using Programmania.ViewModels.Validators;

namespace Programmania.Controllers
{
    [Route("Profile")]
    [Authorize]
    public class ProfileController : Controller
    {
        private IProfileService profileService;

        public ProfileController(ProgrammaniaDBContext dBContext, IProfileService profileService)
        {
            this.profileService = profileService;
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpGet("")]
        public IActionResult Profile()
        {
            var user = HttpContext.Items["User"] as User;
            var userProfileVM = profileService.GetProfileData(user);
            if (userProfileVM != null)
                return View("/Views/Home/Profile.cshtml", userProfileVM);
            else
                return NotFound();
            //return View("", new UserProfileVM(true));
        }

        [AllowAnonymous]
        [HttpGet("show")]
        public IActionResult Profile(string userIdCode)
        {
            int? result = Utilities.Encryptor.DecryptToInt(userIdCode);
            if (result != null)
            {
                UserProfileVM userProfileVM = profileService.GetProfileData(result.Value);
                if (userProfileVM != null)
                    return View("/Views/Home/Profile.cshtml", userProfileVM);
                else
                    return NotFound();
            }
            else
                return NotFound();
            //var profileVM = dBContext.Users.Where(u => u.Id == userId)
            //    .Select(p => new UserProfileVM(false)
            //    {
            //        Nickname = p.Name,
            //        Avatar = fileService.GetSqlFileContext()?.TransactionContext,
            //        Expierence = p.Exp,
            //    });
            //var a = HttpContext.Items["User"] as Models.User;
            //return View("/Views/Home/Profile.cshtml", null);
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpPost("change-nickname")]
        public IActionResult ChangeNickname(NicknameValidator inputs)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.Items["User"] as User;
                int result = profileService.ChangeNickname(user, inputs.Nickname);
                if (result == 1)
                {
                    return Ok();
                }
                else
                {
                    string json = Utilities.FormError.MakeServerError("Nickname", "Nickname not changed please try again");
                    return BadRequest(json);
                }
            }
            else
            {
                string json = Utilities.FormError.MakeModelError(ModelState);
                return BadRequest(json);
            }
        }

        [AllowAnonymous]
        [HttpPost("change-avatar")]
        public IActionResult ChangeNickname(FileValidator inputs)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.Items["User"] as User;
                int result = profileService.ChangeAvatar(user, inputs.File);
                if (result == 1)
                {
                    return Ok();
                }
                else
                {
                    Utilities.FormError.MakeServerError("AvatarError", "Avatar not changed please try again");
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

//[AllowAnonymous]
//[HttpPost("change-password")]
//public IActionResult ChangePassword(ChangePasswordValidator validator)
//{
//    if (ModelState.IsValid)
//    {
//        var user = HttpContext.Items["User"] as User;
//        if (user.Password == validator.OldPassword)
//        {
//            var password = dBContext.Users.FirstOrDefault(u => u.Password == validator.NewPassword);
//            if (password == null)
//            {
//                user.Password = validator.NewPassword;
//                dBContext.SaveChanges();
//                return Ok();
//            }
//            else
//            {
//                return BadRequest();
//            }
//        }
//        else
//        {
//            return BadRequest();
//        }
//    }
//    else
//    {
//        return BadRequest();
//    }
//}
