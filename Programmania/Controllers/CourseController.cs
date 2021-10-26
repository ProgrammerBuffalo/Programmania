using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Programmania.Attributes;
using Programmania.Models;
using Programmania.Services;
using Programmania.ViewModels;

namespace Programmania.Controllers
{
    [Route("Courses")]
    [Authorize]
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

        [HttpGet]
        public IActionResult Courses()
        {
            return View(getCourses(HttpContext.Items["User"] as User));
        }

        [Route("Disciplines")]
        [HttpGet]
        public IActionResult Disciplines(int courseId)
        {
            return View(getDisciplines(HttpContext.Items["User"] as User, courseId));
        }

        [Route("Courses/Disciplines/discipline-begin")]
        [HttpPost]
        public IActionResult BeginDiscipline(int disciplineId)
        {
            var user = HttpContext.Items["User"] as User;
            if (!dbContext.UserDisciplines.Any(ud => ud.DisciplineId == disciplineId && ud.UserId == user.Id))
            {
                Discipline discipline = dbContext.Disciplines.FirstOrDefault(d => d.Id == disciplineId);
                if (discipline == null)
                    return NotFound();
                UserDiscipline userDiscipline = new UserDiscipline { Discipline = discipline, User = user, LessonOrder = 1 };

                dbContext.Update(userDiscipline);
                dbContext.SaveChanges();
                return RedirectToAction("Courses/Disciplines/Lessons", disciplineId);
            }
            return BadRequest();
        }

        [Route("Disciplines/Lessons")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lessons(int disciplineId)
        {
            //UserLessonVM[] userLessonVMs = new UserLessonVM[6];
            //userLessonVMs[0] = new UserLessonVM() { StreamId = Guid.NewGuid(), Id = 1, Order = 4, Name = "lesson5", IsCompleted = false, Test = new TestVM() { A1 = "a", A2 = "b", A3 = "c", A4 = "d", Question = "que??" } };
            //userLessonVMs[1] = new UserLessonVM() { StreamId = Guid.NewGuid(), Id = 2, Order = 5, Name = "lesson6", IsCompleted = false };
            //userLessonVMs[2] = new UserLessonVM() { StreamId = Guid.NewGuid(), Id = 3, Order = 1, Name = "lesson2", IsCompleted = true };
            //userLessonVMs[3] = new UserLessonVM() { StreamId = Guid.NewGuid(), Id = 4, Order = 2, Name = "lesson3", IsCompleted = true };
            //userLessonVMs[4] = new UserLessonVM() { StreamId = Guid.NewGuid(), Id = 5, Order = 3, Name = "lesson4", IsCompleted = false };
            //userLessonVMs[5] = new UserLessonVM() { StreamId = Guid.NewGuid(), Id = 6, Order = 0, Name = "lesson1", IsCompleted = true };
            //return View(null);

            return View(getLessons(HttpContext.Items["User"] as User, disciplineId));
        }

        [Route("Disciplines/Lessons/check-test")]
        [HttpPost]
        public IActionResult CheckTest(int testIndex, int disciplineId, int lessonId)
        {
            User user = HttpContext.Items["User"] as User;
            Lesson lesson = getLessonFromDB(user, disciplineId, lessonId);
            if (lesson == null)
                return NotFound();

            if (lesson.Test.Correct == testIndex)
            {
                UserDiscipline userDiscipline = dbContext.UserDisciplines.FirstOrDefault(ud => ud.DisciplineId == disciplineId && ud.UserId == user.Id);
                if (userDiscipline == null)
                    return NotFound();

                userDiscipline.LessonOrder++;
                dbContext.SaveChanges();

                return Json(true);
            }
            return Json(false);
        }

        [Route("Disciplines/Lessons")]
        [HttpGet]
        public IActionResult CheckLessonAccess(int lessonId, int disciplineId)
        {
            RequestedLessonVM lesson = getRequestedLesson(HttpContext.Items["User"] as User, disciplineId, lessonId);
            if (lesson == null)
                return NotFound();

            return Json(lesson);
        }

        private Lesson getLessonFromDB(User user, int disciplineId, int lessonId)
        {
            int? order = dbContext.UserDisciplines.FirstOrDefault(ud => ud.UserId == user.Id && ud.DisciplineId == disciplineId)?.LessonOrder;
            if (order == null)
                return null;

            Lesson lesson = dbContext.Disciplines.FirstOrDefault(d => d.Id == disciplineId)?.Lessons
                .FirstOrDefault(l => l.Id == lessonId && l.Order <= order);

            return lesson;
        }

        private RequestedLessonVM getRequestedLesson(User user, int disciplineId, int lessonId)
        {
            Lesson lesson = getLessonFromDB(user, disciplineId, lessonId);

            if (lesson == null)
                return null;

            Test test = dbContext.Lessons.FirstOrDefault(l => l.Id == lesson.Id).Test;

            RequestedLessonVM requestedLesson = new RequestedLessonVM
            {
                Test = new TestVM { A1 = test.Answer1, A2 = test.Answer2, A3 = test.Answer3, A4 = test.Answer4, Question = test.Question },
                HTML = new Microsoft.AspNetCore.Html.HtmlString(System.Text.Encoding.UTF8.GetString(
                fileService.GetDocument(dbContext.Documents.FirstOrDefault(d => d.StreamId == lesson.StreamId).Path)))
            };

            return requestedLesson;
        }

        private UserLessonVM[] getLessons(User user, int disciplineId)
        {
            List<UserLessonVM> userLessons = new List<UserLessonVM>();
            UserDiscipline userDiscipline = dbContext.UserDisciplines.Where(u => u.UserId == user.Id).FirstOrDefault(c => c.DisciplineId == disciplineId);

            if (userDiscipline == null)
            {
                return null;
            }

            userLessons = dbContext.Disciplines.FirstOrDefault(d => d.Id == userDiscipline.DisciplineId)?.Lessons.Select(s => new UserLessonVM
            { LessonId = s.Id, Name = s.Name, Order = s.Order, IsCompleted = s.Order <= userDiscipline.LessonOrder ? true : false }).ToList();

             return userLessons.ToArray();
        }

        private UserDisciplineVM[] getDisciplines(User user, int courseId)
        {
            var list = dbContext.UserDisciplines.Include(ud => ud.Discipline).ThenInclude(d => d.Course)
                .Where(u => u.UserId == user.Id && u.Discipline.Course.Id == courseId)
                .Select(s => new
                {
                    DisciplineId = s.DisciplineId,
                    DisciplineName = s.Discipline.Name,
                    LessonsCount = s.Discipline.Lessons.Count,
                    LessonsCompleted = s.LessonOrder,
                    StreamId = s.Discipline.StreamId
                }).ToList();


            List<UserDisciplineVM> userDisciplines = new List<UserDisciplineVM>();

            foreach (var item in list)
            {
                userDisciplines.Add(new UserDisciplineVM
                {
                    DisciplineId = item.DisciplineId,
                    DisciplineName = item.DisciplineName,
                    LessonsCount = item.LessonsCount,
                    LessonsCompleted = item.LessonsCompleted,
                    Image = fileService.GetDocument(dbContext.Documents
                        .FirstOrDefault(d => d.StreamId == item.StreamId).Path)
                });
            }

            List<Discipline> allAvailableDisciplines = dbContext.Disciplines.Where(d => d.Course.Id == courseId).ToList();

            foreach (var item in allAvailableDisciplines)
            {
                if (userDisciplines.Any(d => d.DisciplineId == item.Id))
                    continue;
                userDisciplines.Add(new UserDisciplineVM
                {
                    DisciplineId = item.Id,
                    DisciplineName = item.Name,
                    LessonsCount = item.Lessons.Count,
                    LessonsCompleted = 0,
                    Image = fileService.GetDocument(dbContext.Documents
                        .FirstOrDefault(d => d.StreamId == item.StreamId).Path),
                });
            }

            return userDisciplines.ToArray();

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