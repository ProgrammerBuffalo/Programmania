using Programmania.DAL;
using Programmania.DTOs;
using Programmania.Models;
using Programmania.Services.Interfaces;
using Programmania.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Programmania.Services
{
    public class AdminService : IAdminService
    {
        private ProgrammaniaDBContext db;
        private IFileService fileService;

        public AdminService(ProgrammaniaDBContext db, IFileService fileService)
        {
            this.db = db;
            this.fileService = fileService;
        }

        #region Course
        public int AddCourse(CourseDTO dto)
        {
            if (db.Courses.Any(c => c.Name == dto.Name))
                return 0;

            Course course = new Course();

            SqlFileContext sqlFileContext = fileService.AddEmptyDocument(Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName));
            fileService.FillDocumentContent(sqlFileContext, dto.Image);
            course.StreamId = sqlFileContext.StreamId;

            course.Name = dto.Name;
            course.Description = dto.Description;

            db.Courses.Add(course);
            db.SaveChanges();

            return course.Id;
        }

        public Course GetCourse(int courseId)
        {
            return db.Courses.FirstOrDefault(c => c.Id == courseId);
        }

        public IEnumerable<ListViewModel> GetCourseList()
        {
            return db.Courses.Select(c => new ListViewModel() { Id = c.Id, Name = c.Name });
        }

        public bool UpdateCouse(CourseDTO dto)
        {
            Course course = db.Courses.Where(c => c.Id == dto.CourseId).FirstOrDefault();
            if (course != null)
            {
                if (dto.Image != null)
                {
                    if (course.StreamId != null)
                    {
                        fileService.UpdateDocument(course.StreamId, dto.Image);
                    }
                    else
                    {
                        SqlFileContext sqlFileContext = fileService.AddEmptyDocument(Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName));
                        fileService.FillDocumentContent(sqlFileContext, dto.Image);
                        course.StreamId = sqlFileContext.StreamId;
                    }
                }
                course.Name = dto.Name;
                course.Description = dto.Description;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteCourse(int courseId)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Discipline
        public int AddDiscipline(DisciplineDTO dto)
        {
            if (db.Disciplines.Any(d => d.Name == dto.Name && d.Course.Id == dto.CouseId))
                return 0;

            Discipline discipline = new Discipline();

            SqlFileContext sqlFileContext = fileService.AddEmptyDocument(Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName));
            fileService.FillDocumentContent(sqlFileContext, dto.Image);
            discipline.StreamId = sqlFileContext.StreamId;

            discipline.CourseId = dto.CouseId;
            discipline.Name = dto.Name;
            discipline.Points = dto.Points;

            db.Disciplines.Add(discipline);
            db.SaveChanges();

            return discipline.Id;
        }

        public Discipline GetDiscipline(int disciplineId)
        {
            return db.Disciplines.FirstOrDefault(d => d.Id == disciplineId);
        }

        public IEnumerable<ListViewModel> GetDisciplineList(int courseId)
        {
            return db.Disciplines.Where(d => d.CourseId == courseId)
                                 .Select(d => new ListViewModel() { Id = d.Id, Name = d.Name });
        }

        public bool UpdateDiscipline(DisciplineDTO dto)
        {
            Discipline discipline = db.Disciplines.FirstOrDefault(d => d.Id == dto.DisciplineId);
            if (discipline != null)
            {
                if (dto.Image != null)
                {
                    if (discipline.StreamId != null)
                    {
                        fileService.UpdateDocument(discipline.StreamId, dto.Image);
                    }
                    else
                    {
                        SqlFileContext sqlFileContext = fileService.AddEmptyDocument(Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName));
                        fileService.FillDocumentContent(sqlFileContext, dto.Image);
                        discipline.StreamId = sqlFileContext.StreamId;
                    }
                }
                discipline.Name = dto.Name;
                discipline.Points = dto.Points;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteDiscipline(int disciplineId)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Lesson
        public int AddLesson(AddLessonDTO dto)
        {
            if (db.Lessons.Any(l => l.Name == dto.Name && l.Discipline.Id == dto.DiscipineId))
                return 0;

            Lesson lesson = new Lesson();

            SqlFileContext sqlFileContext = fileService.AddEmptyDocument(Guid.NewGuid().ToString() + ".html");
            fileService.FillDocumentContent(sqlFileContext, dto.Content);
            lesson.StreamId = sqlFileContext.StreamId;

            lesson.DisciplineId = dto.DiscipineId;
            lesson.Name = dto.Name;
            lesson.Order = db.Lessons.Where(l => l.DisciplineId == dto.DiscipineId).Max(l => l.Order);

            db.Lessons.Add(lesson);
            db.SaveChanges();

            return lesson.Id;
        }

        public LessonViewModel GetLesson(int lessonId)
        {
            return db.Lessons.Where(l => l.Id == lessonId)
                             .Select(l => new LessonViewModel()
                             {
                                 Name = l.Name,
                                 Order = l.Order,
                                 Content = System.Text.Encoding.UTF8.GetString(
                                     fileService.GetDocument(db.Documents
                                        .Where(d => d.StreamId == l.StreamId)
                                        .Select(d => d.Path)
                                        .FirstOrDefault()))
                             })
                             .FirstOrDefault();
        }

        public IEnumerable<ListViewModel> GetLessonList(int disciplineId)
        {
            return db.Lessons.Where(l => l.DisciplineId == disciplineId).Select(l => new ListViewModel() { Id = l.Id, Name = l.Name });
        }

        public bool UpdateLesson(int lessonId, string name, int order)
        {
            Lesson lesson = db.Lessons.Where(l => l.Id == lessonId).FirstOrDefault();
            if (lesson == null)
                return false;

            Lesson lesson1 = db.Lessons.Where(l => l.DisciplineId == lesson.DisciplineId && l.Order == order).FirstOrDefault();
            if (lesson1 == null)
                return false;

            lesson.Order = order;
            lesson1.Order = lesson.Order;
            db.SaveChanges();
            return true;
        }

        public bool UpdateContent(int lessonId, string content)
        {
            Guid streamId = db.Lessons.Where(l => l.Id == lessonId).Select(l => l.StreamId).FirstOrDefault();
            if (streamId == null)
                return false;

            fileService.UpdateDocument(streamId, content);

            return true;
        }

        public bool DeleteLesson(int lessonId)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Test
        public int AddTest(AddTestDTO dto)
        {
            Lesson lesson = db.Lessons.Where(l => l.Id == dto.LessonId).FirstOrDefault();
            if (lesson == null)
                return 0;

            Test test = new Test()
            {
                Question = dto.Question,
                Answer1 = dto.Answer1,
                Answer2 = dto.Answer2,
                Answer3 = dto.Answer3,
                Answer4 = dto.Answer4,
                Correct = 1
            };
            db.Tests.Add(test);
            db.SaveChanges();

            lesson.Test = test;
            db.SaveChanges();
            return test.Id;
        }

        public Test GetTest(int testId)
        {
            return db.Tests.Where(t => t.Id == testId).FirstOrDefault();
        }

        public bool UpdateTest(UpdateTestDTO dto)
        {
            Test test = db.Tests.Where(t => t.Id == dto.TestId).FirstOrDefault();
            if (test == null)
                return false;

            test.Question = dto.Question;
            test.Answer1 = dto.Answer1;
            test.Answer2 = dto.Answer2;
            test.Answer3 = dto.Answer3;
            test.Answer4 = dto.Answer4;

            db.SaveChanges();
            return true;
        }

        public bool DeleteTest(int testId)
        {
            throw new System.NotImplementedException();
        }
        #endregion        
    }
}
