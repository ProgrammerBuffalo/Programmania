using Microsoft.AspNetCore.Mvc;
using Programmania.Attributes;
using Programmania.Models;
using Programmania.Services.Interfaces;

namespace Programmania.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private IFileService fileService;
        private IAccountService accountService;

        public HomeController(DAL.ProgrammaniaDBContext context, IFileService fileService, IAccountService accountService)
        {
            this.dbContext = context;
            this.fileService = fileService;
            this.accountService = accountService;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Main")]
        public IActionResult Main()
        {
            var user = HttpContext.Items["User"] as User;
            return View();
        }

        //[HttpGet("get-current-course")]
        //public IActionResult CurrentCourse()
        //{
        //    return Json(true);
        //}

        //[HttpGet("get-performance")]
        //public IActionResult Performance()
        //{
        //    return Json(null);
        //}

        //[HttpGet("get-course")]
        //public IActionResult Courses()
        //{
        //    return Json(null);
        //}

        //[HttpGet("get-info")]
        //public IActionResult Info()
        //{
        //    return Json(null);
        //}
    }
}