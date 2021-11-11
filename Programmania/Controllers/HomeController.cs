using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Programmania.Attributes;
using Programmania.Models;
using Programmania.Services.Interfaces;
using Programmania.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Programmania.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private IFileService fileService;
        private IStaticService staticService;
        private IPerformanceService performanceService;

        public HomeController(DAL.ProgrammaniaDBContext dbContext, IStaticService staticService,
            IFileService fileService, IPerformanceService performanceService)
        {
            this.dbContext = dbContext;
            this.fileService = fileService;
            this.staticService = staticService;
            this.performanceService = performanceService;
        }

        [AllowAnonymous]
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("Main")]
        public IActionResult Main()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("Main/get-user-info")]
        public IActionResult GetUserLevel()
        {
            var user = HttpContext.Items["User"] as User;
            return Json(new UserProfileVM(true)
            {
                Nickname = user.Name,
                Expierence = user.Exp,
                ChallengeStats = dbContext.ChallengeStatistics
                    .Where(cs => cs.UserId == user.Id)
                    .Select(cs => new UserChallengeStatsVM() { Wins = cs.Wins, Draws = cs.Draws, Loses = cs.Loses })
                    .FirstOrDefault()
            });
        }

        [AllowAnonymous]
        [HttpGet("Main/get-user-course")]
        public IActionResult GetUserCourse()
        {
            var user = HttpContext.Items["User"] as User;

            UserDiscipline userDiscipline = dbContext.UserDisciplines.Include(ud => ud.Discipline)
                .Where(ud => ud.UserId == user.Id)
                .OrderByDescending(d => d.LastDate).FirstOrDefault();

            if (userDiscipline == null)
            {
                return Json(null);
            }

            Course course = dbContext.UserDisciplines.Find(userDiscipline).Discipline.Course;

            UserDiscipline[] userDisciplines = dbContext.UserDisciplines.Include(ud => ud.Discipline).ThenInclude(ud => ud.Course).
                                Where(ud => ud.UserId == user.Id && ud.Discipline.Course.Id == course.Id).ToArray();

            UserCourseVM userCourseVM = new UserCourseVM
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Description = course.Description,
                LessonsCount = course.LessonCount,
                Image = fileService.GetDocument(dbContext.Documents
                .FirstOrDefault(d => d.StreamId == course.StreamId)?.Path),
                LessonsCompleted = 0
            };

            foreach (var item in userDisciplines)
            {
                userCourseVM.LessonsCompleted += item.LessonOrder;
            }

            return Json(new
            {
                CurrentCourse = userCourseVM,
                CurrentDiscipline = userDiscipline.Discipline.Name,
                CurrentDisciplineId = userDiscipline.Discipline.Id
            });
        }

        [AllowAnonymous]
        [HttpGet("Main/get-all-courses")]
        public IActionResult GetAllCourses()
        {
            var user = HttpContext.Items["User"] as User;

            UserCourseVM[] userCourses = staticService.GetCourses(user, fileService);
            return Json(userCourses);
        }

        [AllowAnonymous]
        [HttpGet("Main/get-user-performance")]
        public IActionResult GetUserPerformance()
        {
            var user = HttpContext.Items["User"] as User;
            if (user != null)
            {
                IEnumerable<Reward> rewards = performanceService.GetRewards(user, 30, 0);
                return Json(rewards);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("Main/get-offered-challenges")]
        public IActionResult GetOfferedChallenges()
        {
            var user = HttpContext.Items["User"] as User;

            List<OfferedChallengeVM> offeredChallenges = staticService.GetOfferedChallenges(user, fileService);
            return Json(offeredChallenges);
        }

        [AllowAnonymous]
        [HttpGet("Main/get-possible-challenges")]
        public IActionResult GetPossibleChallenges(int count)
        {
            var user = HttpContext.Items["User"] as User;

            List<PossibleChallengeVM> possibleChallenges = staticService.GetPossibleChallenges(fileService, count);
            return Json(possibleChallenges);
        }
    }
}