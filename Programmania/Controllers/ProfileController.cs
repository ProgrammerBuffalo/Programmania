using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Programmania.Controllers
{
    [AllowAnonymous]
    [Route("Profile")]
    public class ProfileController : Controller
    {
        [Route("")]
        [AllowAnonymous]
        public IActionResult Profile()
        {
            //check this User or another
            //ViewModels.UserProfileVM profileVM = new ViewModels.UserProfileVM(true);
            //return View(profileVM);
            return View(new ViewModels.UserProfileVM(true));
        }

        [HttpGet]
        [Route("change-nickname")]
        [AllowAnonymous]
        public IActionResult ChangeNickname(string a)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("change-avatar")]
        [AllowAnonymous]
        public IActionResult ChangeNickname([Attributes.FileValidation(1000000, ErrorMessage = "")]
            Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("get-games")]
        [AllowAnonymous]
        public IActionResult GetGames()
        {
            return Json(null);
        }

        [Route("get-achivments")]
        [AllowAnonymous]
        public IActionResult GetAchivments()
        {
            return Json(null);
        }

        [Route("get-user-info")]
        [AllowAnonymous]
        public IActionResult GetUserInfo()
        {
            return Json(null);
        }

        [HttpGet]
        [Route("temp")]
        [AllowAnonymous]
        public IActionResult Temp(string data1)
        {
            return Ok();
        }
    }
}
