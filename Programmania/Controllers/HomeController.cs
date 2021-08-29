using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Programmania.Models;
using Programmania.Services;
using Programmania.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Programmania.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private IFileService fileService;

        public HomeController(DAL.ProgrammaniaDBContext context, IFileService file_service)
        {
            this.dbContext = context;
            this.fileService = file_service;
        }

        [AllowAnonymous]
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
            var user = getUser(HttpContext.User.Claims.ToList());

            if (user == null)
            {
                return NotFound("Token is not valid");
            }

            return View(getUserCourses(user));
        }

        [Route("Profile")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Profile()
        {
            var user = getUser(HttpContext.User.Claims.ToList());

            if (user == null)
            {
                return NotFound("Token invalid");
            }

            return View(new UserProfileVM(true, user.Login, user.Name, user.Exp, getUserCourses(user), getUserAchievements(user)));
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
            var user = dbContext.Users
                .FirstOrDefault(u => u.Id == int.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value) &&
                                     u.Login == claims.First(c => c.Type == ClaimTypes.Name).Value);
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
                    FormFile = fileService.GetDocument(dbContext.Documents
                        .FirstOrDefault(d => d.StreamId == s.StreamId).Path)
                }).ToList();

            return list;
        }

        private List<UserCourseVM> getUserCourses(User user)
        {
            var list = dbContext.UserDisciplines.Where(u => u.UserId == user.Id)
                  .Join(dbContext.Courses, userDiscipline => userDiscipline.DisciplineId,
                                 course => course.DisciplineId,
                                 (userDiscipline, course) => new
                                 {
                                     Discipline = userDiscipline.Discipline,
                                     Course = course,
                                     LastLesson = userDiscipline.LessonOrder,
                                     LessonCount = course.LessonCount,
                                     StreamIdCourse = course.StreamId
                                 }).Select(s => new
                                 {
                                     discipline = s.Discipline,
                                     course = s.Course,
                                     lastLesson = s.LastLesson,
                                     lessonCount = s.LessonCount,
                                     streamId = s.StreamIdCourse
                                 }).ToList();

            List<UserCourseVM> userCourses = new List<UserCourseVM>();

            foreach (var item in list)
            {
                var userCourse = userCourses.FirstOrDefault(uc => uc.CourseId == item.course.Id);
                if (userCourse == null)
                {
                    userCourses.Add(new UserCourseVM
                    {
                        CourseId = item.course.Id,
                        CourseName = item.course.Name,
                        LessonsCount = item.lessonCount,
                        LessonsCompleted = item.lastLesson,
                        FormFile = fileService.GetDocument(dbContext.Documents
                        .FirstOrDefault(d => d.StreamId == item.streamId).Path)
                    });
                }
                else
                {
                    userCourse.LessonsCompleted += item.lastLesson;
                }
            }

            return userCourses;
        }
    }
}