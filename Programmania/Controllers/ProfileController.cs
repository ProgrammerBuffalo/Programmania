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

        //AllowAnonymous is allowed for this action
        [Route("change-image")]
        [AllowAnonymous]
        public IActionResult ChangeImage(string image)
        {
            return Ok();
        }

        //AllowAnonymous is allowed for this action
        [Route("change-nickname")]
        [AllowAnonymous]
        public IActionResult ChangeNickname(string nickname)
        {
            return Ok();
        }
    }
}
