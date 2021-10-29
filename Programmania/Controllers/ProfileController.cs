using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Programmania.Controllers
{
    public class ProfileController : Controller
    {
        [Route("")]
        [AllowAnonymous]
        public IActionResult Profile()
        {
            //check this User or another
            return View(false);
        }

        [Route("change-nickname")]
        [AllowAnonymous]
        public IActionResult ChangeImage(string nickname)
        {
            return Ok();
        }

        [Route("change-avatar")]
        [AllowAnonymous]
        public IActionResult ChangeNickname(Microsoft.AspNetCore.Http.IFormFile file)
        {
            return Ok();
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
    }
}
