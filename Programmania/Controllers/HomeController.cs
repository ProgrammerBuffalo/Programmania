using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Programmania.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Route("Registrate")]
        [HttpPost]
        public IActionResult MakeRegister()
        {
            return View("Main");
        }

        [Route("Main")]
        public IActionResult Main()
        {
            return View();
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
