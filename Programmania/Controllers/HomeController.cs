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
        private IStaticService staticService;

        public HomeController(DAL.ProgrammaniaDBContext context, IStaticService staticService, 
            IFileService file_service, IAccountService accountService)
        {
            this.dbContext = context;
            this.fileService = fileService;
            this.accountService = accountService;
            this.staticService = staticService;
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
            return View();
        }

        [Route("Main/get-user-course")]
        [HttpGet]
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
                CourseId = course.Id, CourseName = course.Name,
                Description = course.Description, LessonsCount = course.LessonCount,
                Image = fileService.GetDocument(dbContext.Documents
                .FirstOrDefault(d => d.StreamId == course.StreamId)?.Path),
                LessonsCompleted = 0
            };

            foreach(var item in userDisciplines)
            {
                userCourseVM.LessonsCompleted += item.LessonOrder;
            }

            return Json(new { CurrentCourse = userCourseVM, CurrentDiscipline = userDiscipline.Discipline.Name });
        }

        [Route("Main/get-all-courses")]
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var user = HttpContext.Items["User"] as User;

            UserCourseVM[] userCourses = staticService.GetCourses(user, fileService);
            return Json(userCourses);
        }

        [Route("Main/get-user-level")]
        [HttpGet]
        public IActionResult GetUserLevel()
        {
            var user = HttpContext.Items["User"] as User;

            return Json(new { Expierence = user.Exp, Level = (int)(System.Math.Sqrt(user.Exp) / 150) });
        }

        [Route("Main/get-user-performance")]
        [HttpGet]
        public IActionResult GetUserPerformance()
        {
            return Json("test");
        }

        [Route("Main/get-offered-challenges")]
        [HttpGet]
        public IActionResult GetOfferedChallenges()
        {
            var user = HttpContext.Items["User"] as User;

            List<OfferedChallengeVM> offeredChallenges = staticService.GetOfferedChallenges(user, fileService);
            return Json(offeredChallenges);
        }

        [Route("Main/get-possible-challenges")]
        [HttpGet]
        public IActionResult GetPossibleChallenges(int count)
        {
            var user = HttpContext.Items["User"] as User;

            List<PossibleChallengeVM> possibleChallenges = staticService.GetPossibleChallenges(fileService, count);
            return Json(possibleChallenges);
        }
    }
}