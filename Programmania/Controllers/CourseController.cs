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

        public IActionResult Courses()
        {
            //return /*View(getUserCourses(HttpContext.Items["User"] as User))*/ View();
            return View();
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
                        Image = fileService.GetDocument(dbContext.Documents
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