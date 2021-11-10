using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Programmania.Attributes;
using Programmania.Models;
using Programmania.Services.Interfaces;
using Programmania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Programmania.Controllers
{
    [Route("Courses")]
    //[Authorize]
    public class CourseController : Controller
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private IFileService fileService;
        private IStaticService staticService;

        public CourseController(DAL.ProgrammaniaDBContext context, IStaticService staticService, IFileService fileService)
        {
            this.dbContext = context;
            this.staticService = staticService;
            this.fileService = fileService;
        }

        [HttpGet("")]
        public IActionResult Courses()
        {
            UserCourseVM[] userCourses = staticService.GetCourses(HttpContext.Items["User"] as User, fileService);
            return View(userCourses);
        }

        [HttpGet("Disciplines")]
        [AllowAnonymous]
        public IActionResult Disciplines(int courseId)
        {
            //UserDisciplineVM[] userDisciplines = new UserDisciplineVM[6];
            //userDisciplines[0] = new UserDisciplineVM() { DisciplineName = "discp1", DisciplineId = 10, LessonsCount = 100, LessonsCompleted = 24, Image = System.IO.File.ReadAllBytes("wwwroot\\images\\caio.jpg") };
            //userDisciplines[1] = new UserDisciplineVM() { DisciplineName = "discp1", DisciplineId = 20, LessonsCount = 100, LessonsCompleted = 24, Image = System.IO.File.ReadAllBytes("wwwroot\\images\\caio.jpg") };
            //userDisciplines[2] = new UserDisciplineVM() { DisciplineName = "discp1", DisciplineId = 30, LessonsCount = 100, LessonsCompleted = 24, Image = System.IO.File.ReadAllBytes("wwwroot\\images\\caio.jpg") };
            //userDisciplines[3] = new UserDisciplineVM() { DisciplineName = "discp1", DisciplineId = 40, LessonsCount = 100, LessonsCompleted = 24, Image = System.IO.File.ReadAllBytes("wwwroot\\images\\caio.jpg") };
            //userDisciplines[4] = new UserDisciplineVM() { DisciplineName = "discp1", DisciplineId = 50, LessonsCount = 100, LessonsCompleted = 24, Image = System.IO.File.ReadAllBytes("wwwroot\\images\\caio.jpg") };
            //userDisciplines[5] = new UserDisciplineVM() { DisciplineName = "discp1", DisciplineId = 60, LessonsCount = 100, LessonsCompleted = 24, Image = System.IO.File.ReadAllBytes("wwwroot\\images\\caio.jpg") };
            return View(getDisciplines(HttpContext.Items["User"] as User, courseId));
            //return View(userDisciplines);
        }

        [HttpGet("Disciplines/get-course-description")]
        public IActionResult GetCourseShortDescription(int courseId)
        {
            Course course = dbContext.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return NotFound();

            var image = fileService.GetDocument(dbContext.Documents
                .FirstOrDefault(d => d.StreamId == course.StreamId)?.Path);
            var name = course.Name;

            var info = new { Image = image, Name = name };

            return Json(info);
        }

        [HttpGet("Disciplines/Lessons/get-discipline-description")]
        public IActionResult GetDisciplineShortDescription(int disciplineId)
        {
            Discipline discipline = dbContext.Disciplines.FirstOrDefault(c => c.Id == disciplineId);

            if (discipline == null)
                return NotFound();

            var image = fileService.GetDocument(dbContext.Documents
                .FirstOrDefault(d => d.StreamId == discipline.StreamId)?.Path);
            var name = discipline.Name;

            var info = new { Image = image, Name = name };

            return Json(info);
        }

        [HttpGet("Disciplines/get-course-description")]
        public IActionResult SelectedCourseInfo()
        {
            //this method should return image and name of selected course
            //var data = new { CourseName = "hello1", CourseImage = new byte[0] };
            return Json(null);
        }

        [Route("Disciplines/discipline-begin")]
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
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("Disciplines/Lessons")]
        public IActionResult Lessons(int disciplineId)
        {
            return View(getLessons(HttpContext.Items["User"] as User, disciplineId));
        }

        [HttpGet("Disciplines/Lessons/get-discipline-description")]
        public IActionResult SelectedDisciplineInfo()
        {
            //this method should return image and name of selected discipline
            //var data = new { DisciplineName = "", DisciplineImage = new byte[0] };
            return Json(null);
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

        [AllowAnonymous]
        [Route("Disciplines/Lesson")]
        [HttpGet]
        public IActionResult CheckLessonAccess(int lessonId, int disciplineId)
        {
            return Json(null);
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
                HTML = System.Text.Encoding.UTF8.GetString(fileService.GetDocument(
                    dbContext.Documents.FirstOrDefault(d => d.StreamId == lesson.StreamId)?.Path))
            };

            return requestedLesson;
        }

        private UserLessonVM[] getLessons(User user, int disciplineId)
        {
            List<UserLessonVM> userLessons = new List<UserLessonVM>();
            UserDiscipline userDiscipline = dbContext.UserDisciplines.Where(u => u.UserId == user.Id).
                FirstOrDefault(c => c.DisciplineId == disciplineId);

            if (userDiscipline == null)
            {
                return null;
            }

            userLessons = dbContext.Disciplines.FirstOrDefault(d => d.Id == userDiscipline.DisciplineId)?.Lessons.Select(s => new UserLessonVM
            { LessonId = s.Id, Name = s.Name, Order = s.Order, IsCompleted = s.Order <= userDiscipline.LessonOrder ? true : false }).ToList();

            userDiscipline.LastDate = DateTime.Now;
            dbContext.SaveChanges();

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
                        .FirstOrDefault(d => d.StreamId == item.StreamId)?.Path)
                });
            }

            List<Discipline> allAvailableDisciplines = dbContext.Disciplines.Include(d => d.Lessons).Where(d => d.Course.Id == courseId).ToList();

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
                        .FirstOrDefault(d => d.StreamId == item.StreamId)?.Path),
                });
            }

            return userDisciplines.ToArray();

        }


    }
}