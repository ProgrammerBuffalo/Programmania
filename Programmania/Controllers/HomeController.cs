using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Programmania.Attributes;
using Programmania.Models;
using Programmania.Services;
using Programmania.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Programmania.Controllers
{
    [Route("Home")]
    [Route("")]
    [Authorize]
    public class HomeController : Controller
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private IFileService fileService;
        private IAccountService accountService;

        public HomeController(DAL.ProgrammaniaDBContext context, IFileService file_service, IAccountService accountService)
        {
            this.dbContext = context;
            this.fileService = file_service;
            this.accountService = accountService;
        }

        [Route("")]
        [Route("Index")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Main")]
        [HttpGet]
        public IActionResult Main()
        {
            return View();
        }

        [Route("Profile")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Profile()
        {
            //var user = getUser(HttpContext.User.Claims.ToList());

            //if (user == null)
            //{
            //  return NotFound("Token invalid");
            //}

            return View(new UserProfileVM(true));
            //return View(/*new UserProfileVM(true, user.Login, user.Name, user.Exp, getUserCourses(user), getUserAchievements(user))*/);
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

        private User getUser(List<Claim> claims)
        {
            var users = dbContext.Users;

            if (claims.Count < 1)
                return null;

            var user = dbContext.Users.AsEnumerable().FirstOrDefault(u => u.Id == int.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value) && u.Login == claims.First(c => c.Type == ClaimTypes.Name).Value);
            return user;
        }

        private List<UserAchievementVM> getUserAchievements(User user)
        {
            List<UserAchievementVM> list = dbContext.Users.Include(u => u.Achievements)
                .First(u => u == user).Achievements.Select(s => new UserAchievementVM
                {
                    Name = s.Name,
                    Description = s.Desc,
                    Points = s.Points,
                    Image = fileService.GetDocument(dbContext.Documents
                        .FirstOrDefault(d => d.StreamId == s.StreamId).Path)
                }).ToList();

            return list;
        }


    }
}