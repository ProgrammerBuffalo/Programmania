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

        [AllowAnonymous]
        //[Authorize]
        [HttpGet("")]
        public IActionResult Profile(string userIdCode)
        {
            //UserProfileVM profileVM;
            //if (userIdCode == null)
            //{
            //    var user = HttpContext.Items["User"] as User;
            //    profileVM = profileService.GetProfileData(user);
            //}
            //else
            //{
            //    int? result = Utilities.Encryptor.DecryptToInt(userIdCode);
            //    if (result != null)
            //        profileVM = profileService.GetProfileData(result.Value);
            //    else
            //        return NotFound();
            //}
            //if (profileVM != null)
            //    return View("/Views/Home/Profile.cshtml", profileVM);
            //else
            //    return NotFound();
            return View("/Views/Home/Profile.cshtml", new UserProfileVM(true));
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpGet("get-games")]
        public IActionResult GetGames(string userIdCode)
        {
            int? userId;
            if (userIdCode == null)
                userId = Utilities.Encryptor.DecryptToInt(userIdCode);
            else
            {
                var user = HttpContext.Items["User"] as User;
                userId = user.Id;
            }
            return Json(null);
            //return Json(profileService.GetGames(userId.Value));
        }

        [Authorize]
        [HttpGet("get-achivments")]
        public IActionResult GetAchivments(string uerIdCode)
        {
            return Json(null);
        }

        [Authorize]
        [HttpGet("get-user-info")]
        public IActionResult GetUserInfo(string userIdCode)
        {
            int? userId;
            if (userIdCode == null)
                userId = Utilities.Encryptor.DecryptToInt(userIdCode);
            else
            {
                var user = HttpContext.Items["User"] as User;
                userId = user.Id;
            }
            return Json(null);
            //return Json(profileService.GetInfo(userId.Value));
        }
    }
}