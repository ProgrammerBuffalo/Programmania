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
            return db.Disciplines.FirstOrDefault(d =>  d.Id == disciplineId);
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

        public string GetLesson(int lessonId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ListViewModel> GetLessonList(int disciplineId)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateLesson(string lesson)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteLesson(int lessonId)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Test
        public int AddTest(TestDTO test)
        {
            throw new System.NotImplementedException();
        }

        public Test GetTest(int testId)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateTest(TestDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteTest(int testId)
        {
            throw new System.NotImplementedException();
        }
        #endregion        
    }
}
