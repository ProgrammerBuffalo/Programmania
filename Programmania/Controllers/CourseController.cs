using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Programmania.Attributes;
using Programmania.Models;
using Programmania.Services;
using Programmania.ViewModels;

namespace Programmania.Controllers
{
    [Route("Courses")]
    public class CourseController : Controller
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private IAccountService accountService;
        private IXMLService xmlService;
        private IFileService fileService;

        public CourseController(DAL.ProgrammaniaDBContext context, IAccountService accountService,
    IXMLService xmlService, IFileService fileService)
        {
            this.dbContext = context;
            this.accountService = accountService;
            this.xmlService = xmlService;
            this.fileService = fileService;
        }
        
        [Authorize]
        public IActionResult Courses()
        {
            return View(getCourses(HttpContext.Items["User"] as User));
        }

        [Route("Course/Disciplines")]
        public IActionResult Disciplines(int courseId)
        {
            var token = Request.Cookies["JwtToken"];

            //var claims = accountService.GetClaimsFromJWTToken();



            //var user = dbContext.Users.FirstOrDefault(u => u.Id == int.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value)
            //       && u.Login == claims.First(c => c.Type == ClaimTypes.Name).Value);

            //var list = dbContext.UserDisciplines.Where(u => u.UserId == user.Id && u.Discipline.Course.Id == courseId)
            //.Join(dbContext.Lessons, userDiscipline => userDiscipline.DisciplineId, lesson => lesson.DisciplineId,
            //(userDiscipline, lesson) => new
            //{
            //Discipline = userDiscipline.Discipline,
            //Lesson = lesson,   
            //}).Select(s => new
            //{

            //})


            return View();
        }

        private UserCourseVM[] getCourses(User user)
        {
            var list = dbContext.UserDisciplines.Where(u => u.UserId == user.Id)
                  .Join(dbContext.Disciplines, userDiscipline => userDiscipline.DisciplineId,
                                 discipline => discipline.Id,
                                 (userDiscipline, discipline) => new
                                 {
                                     Discipline = userDiscipline.Discipline,
                                     Course = discipline.Course,
                                     LastLesson = userDiscipline.LessonOrder,
                                     LessonCount = discipline.Course.LessonCount,
                                     StreamIdCourse = discipline.Course.StreamId
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
                        IsSelected = true,
                        Image = fileService.GetDocument(dbContext.Documents
                        .FirstOrDefault(d => d.StreamId == item.streamId).Path)
                    });
                }
                else
                {
                    userCourse.LessonsCompleted += item.lastLesson;
                }
            }

            List<Course> allAvailableCourses = dbContext.Courses.ToList();

            foreach (var item in allAvailableCourses)
            {
                if (userCourses.Any(uc => uc.CourseId == item.Id))
                    continue;
                userCourses.Add(new UserCourseVM
                {
                    CourseId = item.Id,
                    CourseName = item.Name,
                    IsSelected = false,
                    LessonsCount = item.LessonCount,
                    Image = fileService.GetDocument(dbContext.Documents
                        .FirstOrDefault(d => d.StreamId == item.StreamId).Path),
                    LessonsCompleted = 0
                });
            }

            return userCourses.ToArray();
        }
    }
}