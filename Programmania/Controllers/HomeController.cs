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
            //return Redirect("Main");
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
                Nickname = "nickname",
                Expierence = 110000,
                ChallengeStats = new UserChallengeStatsVM() { Wins = 10, Draws = 2, Loses = 4 }
            });
            //return Json(new UserProfileVM(true)
            //{
            //    Nickname = user.Name,
            //    Expierence = user.Exp,
            //    ChallengeStats = dbContext.ChallengeStatistics
            //        .Where(cs => cs.UserId == user.Id)
            //        .Select(cs => new UserChallengeStatsVM() { Wins = cs.Wins, Draws = cs.Draws, Loses = cs.Loses })
            //        .FirstOrDefault()
            //});
        }

        [AllowAnonymous]
        [HttpGet("Main/get-user-course")]
        public IActionResult GetUserCourse()
        {
            //var user = HttpContext.Items["User"] as User;

            //UserDiscipline userDiscipline = dbContext.UserDisciplines.Include(ud => ud.Discipline)
            //    .Where(ud => ud.UserId == user.Id)
            //    .OrderByDescending(d => d.LastDate).FirstOrDefault();

            //if (userDiscipline == null)
            //{
            //    return Json(null);
            //}

            //Course course = dbContext.UserDisciplines.Find(userDiscipline).Discipline.Course;

            //UserDiscipline[] userDisciplines = dbContext.UserDisciplines.Include(ud => ud.Discipline).ThenInclude(ud => ud.Course).
            //                    Where(ud => ud.UserId == user.Id && ud.Discipline.Course.Id == course.Id).ToArray();

            //UserCourseVM userCourseVM = new UserCourseVM
            //{
            //    CourseId = course.Id,
            //    CourseName = course.Name,
            //    Description = course.Description,
            //    LessonsCount = course.LessonCount,
            //    Image = fileService.GetDocument(dbContext.Documents
            //    .FirstOrDefault(d => d.StreamId == course.StreamId)?.Path),
            //    LessonsCompleted = 0
            //};

            //foreach (var item in userDisciplines)
            //{
            //    userCourseVM.LessonsCompleted += item.LessonOrder;
            //}

            //return Json(new
            //{
            //    CurrentCourse = userCourseVM,
            //    CurrentDiscipline = userDiscipline.Discipline.Name,
            //    CurrentDisciplineId = userDiscipline.Discipline.Id
            //});

            return Json(new
            {
                CurrentCourse = new UserCourseVM()
                {
                    CourseId = 2,
                    CourseName = "name1",
                    Description = "desc1",
                    LessonsCount = 100,
                    LessonsCompleted = 20,
                    Image = System.IO.File.ReadAllBytes("wwwroot\\images\\AngularLogo.png")
                },
                CurrentDiscipline = "discile name",
                CurrentDisciplineId = 1
            });
        }

        [AllowAnonymous]
        [HttpGet("Main/get-all-courses")]
        public IActionResult GetAllCourses()
        {
            //var user = HttpContext.Items["User"] as User;

            //UserCourseVM[] userCourses = staticService.GetCourses(user, fileService);
            //return Json(userCourses);

            UserCourseVM[] userCourses = new UserCourseVM[2];
            userCourses[0] = new UserCourseVM()
            {
                CourseId = 1,
                Description = "desc1",
                LessonsCount = 13,
                LessonsCompleted = 10,
                CourseName = "course1",
                Image = System.IO.File.ReadAllBytes("wwwroot\\images\\AngularLogo.png")
            };
            userCourses[1] = new UserCourseVM()
            {
                CourseId = 2,
                Description = "desc2",
                LessonsCount = 16,
                LessonsCompleted = 10,
                CourseName = "course2",
                Image = System.IO.File.ReadAllBytes("wwwroot\\images\\AngularLogo.png")
            };
            return Json(userCourses);
        }

        [AllowAnonymous]
        [HttpGet("Main/get-user-performance")]
        public IActionResult GetUserPerformance()
        {
            //var user = HttpContext.Items["User"] as User;
            //if (user != null)
            //{
            //    IEnumerable<Reward> rewards = performanceService.GetRewards(user, 30, 0);
            //    return Json(rewards);
            //}
            //return BadRequest();

            Reward[] rewards = new Reward[10];
            rewards[0] = new Reward() { Id = 1, Description = "asd", Experience = 1, CreatedAt = System.DateTime.Now };
            rewards[1] = new Reward() { Id = 2, Description = "asd1", Experience = 2, CreatedAt = System.DateTime.Now };
            rewards[2] = new Reward() { Id = 3, Description = "asd2", Experience = 3, CreatedAt = System.DateTime.Now };
            rewards[3] = new Reward() { Id = 4, Description = "asd3", Experience = 4, CreatedAt = System.DateTime.Now };
            rewards[4] = new Reward() { Id = 5, Description = "asd4", Experience = 5, CreatedAt = System.DateTime.Now };
            rewards[5] = new Reward() { Id = 6, Description = "asd", Experience = 6, CreatedAt = System.DateTime.Now };
            rewards[6] = new Reward() { Id = 7, Description = "asd5", Experience = 7, CreatedAt = System.DateTime.Now };
            rewards[7] = new Reward() { Id = 8, Description = "asd6", Experience = 8, CreatedAt = System.DateTime.Now };
            rewards[8] = new Reward() { Id = 9, Description = "asd", Experience = 9, CreatedAt = System.DateTime.Now };
            rewards[9] = new Reward() { Id = 10, Description = "asd7", Experience = 10, CreatedAt = System.DateTime.Now };
            return Json(rewards);
        }

        [AllowAnonymous]
        [HttpGet("Main/get-offered-challenges")]
        public IActionResult GetOfferedChallenges()
        {
            //var user = HttpContext.Items["User"] as User;

            //List<OfferedChallengeVM> offeredChallenges = staticService.GetOfferedChallenges(user, fileService);
            //return Json(offeredChallenges);

            OfferedChallengeVM[] offeredChallengeVMs = new OfferedChallengeVM[2];
            offeredChallengeVMs[0] = new OfferedChallengeVM()
            {
                Id = 2,
                Course = "course1",
                Date = System.DateTime.Now,
                CourseAvatar = System.IO.File.ReadAllBytes("wwwroot\\images\\AngularLogo.png"),
                OpponentDescription = new UserShortDescriptionVM()
                {
                    Id = 11,
                    Name = "name1",
                    Avatar = System.IO.File.ReadAllBytes("wwwroot\\images\\caio.jpg")
                }
            };
            offeredChallengeVMs[1] = new OfferedChallengeVM()
            {
                Id = 2,
                Course = "course2",
                Date = System.DateTime.Now,
                CourseAvatar = System.IO.File.ReadAllBytes("wwwroot\\images\\AngularLogo.png"),
                OpponentDescription = new UserShortDescriptionVM()
                {
                    Id = 11,
                    Name = "name2",
                    Avatar = System.IO.File.ReadAllBytes("wwwroot\\images\\caio.jpg")
                }
            };
            return Json(offeredChallengeVMs);
        }

        [AllowAnonymous]
        [HttpGet("Main/get-possible-challenges")]
        public IActionResult GetPossibleChallenges(int count)
        {
            //var user = HttpContext.Items["User"] as User;

            //List<PossibleChallengeVM> possibleChallenges = staticService.GetPossibleChallenges(fileService, count);
            //return Json(possibleChallenges);

            PossibleChallengeVM[] possibleChallengeVM = new PossibleChallengeVM[2];
            possibleChallengeVM[0] = new PossibleChallengeVM()
            {
                Course = "course1",
                CourseAvatar = System.IO.File.ReadAllBytes("wwwroot\\images\\AngularLogo.png"),
                OpponentDescription = new UserShortDescriptionVM()
                {
                    Id = 11,
                    Name = "name1",
                    Avatar = System.IO.File.ReadAllBytes("wwwroot\\images\\caio.jpg")
                }
            };
            possibleChallengeVM[1] = new PossibleChallengeVM()
            {
                Course = "course2",
                CourseAvatar = System.IO.File.ReadAllBytes("wwwroot\\images\\AngularLogo.png"),
                OpponentDescription = new UserShortDescriptionVM()
                {
                    Id = 11,
                    Name = "name2",
                    Avatar = System.IO.File.ReadAllBytes("wwwroot\\images\\caio.jpg")
                }
            };
            return Json(possibleChallengeVM);
        }
    }
}