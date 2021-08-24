using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Programmania.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Route("Main")]
        [HttpGet]
        //change string parameter into ViewModel class
        public IActionResult Main(ViewModels.AuthenticationRequestVM data)
        {
            //check if email exists in db
            if (false)
            {
                return BadRequest();
            }
            else
            { 
                return View();
            }
        }

        [Route("Courses")]
        public IActionResult Courses()
        {
            return View();
        }

        [Route("News")]
        public IActionResult News()
        {
            return View();
        }

        [Route("News/PostNews")]
        public IActionResult PostNews()
        {
            return View();
        }

        [Route("Profile")]
        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("Codes")]
        public IActionResult Codes()
        {
            return View();
        }

        [Route("Challenges")]
        public IActionResult Challenges()
        {
            return View();
        }

        [Route("Challenges/Game")]
        public IActionResult Game()
        {
            return View();
        }
    }
}