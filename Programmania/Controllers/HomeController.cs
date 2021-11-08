using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Programmania.Attributes;
using Programmania.Models;
using Programmania.Services.Interfaces;
using Programmania.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Programmania.Controllers
{
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

        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Main")]
        [HttpGet]
        [Authorize]
        public IActionResult Main()
        {
            var user = HttpContext.Items["User"] as User;   

            return View();
        }

    }
}