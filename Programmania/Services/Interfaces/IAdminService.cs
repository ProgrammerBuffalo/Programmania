using Programmania.DTOs;
using Programmania.Models;
using Programmania.ViewModels;
using System.Collections.Generic;

namespace Programmania.Services.Interfaces
{
    public interface IAdminService
    {
        #region Course
        int AddCourse(CourseDTO course);
        IEnumerable<ListViewModel> GetCourseList();
        Course GetCourse(int courseId);
        bool UpdateCouse(CourseDTO dto);
        bool DeleteCourse(int courseId);
        #endregion

        #region Discipline
        int AddDiscipline(DisciplineDTO discipline);
        IEnumerable<ListViewModel> GetDisciplineList(int courseId);
        Discipline GetDiscipline(int disciplineId);
        bool UpdateDiscipline(DisciplineDTO discipline);
        bool DeleteDiscipline(int disciplineId);
        #endregion

        #region Lesson
        int AddLesson(AddLessonDTO lesson);
        IEnumerable<ListViewModel> GetLessonList(int disciplineId);
        LessonViewModel GetLesson(int lessonId);
        bool UpdateLesson(int lessonId, string name, int order);
        bool UpdateContent(int lessonId, string content);
        bool DeleteLesson(int lessonId);
        #endregion

        #region Test
        int AddTest(AddTestDTO test);
        Test GetTest(int testId);
        bool UpdateTest(UpdateTestDTO dto);
        bool DeleteTest(int testId);
        #endregion
    }
}
