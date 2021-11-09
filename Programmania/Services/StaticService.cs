using Programmania.Models;
using Programmania.Services.Interfaces;
using Programmania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Programmania.Services
{
    public class StaticService : IStaticService
    {
        private DAL.ProgrammaniaDBContext dbContext;

        public StaticService(DAL.ProgrammaniaDBContext context)
        {
            this.dbContext = context;
        }

        public UserCourseVM[] GetCourses(User user, IFileService fileService)
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
                        .FirstOrDefault(d => d.StreamId == item.streamId)?.Path)
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
                        .FirstOrDefault(d => d.StreamId == item.StreamId)?.Path),
                    LessonsCompleted = 0
                });
            }

            return userCourses.ToArray();
        }

        public List<OfferedChallengeVM> GetOfferedChallenges(User user, IFileService fileService)
        {
            List<OfferedChallengeVM> challenges =
                dbContext.UserChallenges.Where(uc => uc.UserId == user.Id && !uc.IsFinished).
                                         Select(s => new OfferedChallengeVM
                                         {
                                             Id = s.ChallengeId,
                                             Course = s.Challenge.Course.Name,
                                             Date = s.Challenge.Created,
                                             OpponentDescription = new UserShortDescriptionVM
                                             {
                                                 Id = s.UserId,
                                                 Name = s.User.Name,
                                                 Avatar = fileService.GetDocument(dbContext.Documents
                                                                     .FirstOrDefault(d => d.StreamId == s.User.ImageId).Path)
                                             }
                                         }).ToList();
            return challenges;
        }

        public List<PossibleChallengeVM> GetPossibleChallenges(IFileService fileService, int count)
        {
            Random random = new Random();
            List<User> opponents = dbContext.Users.OrderBy(u => Guid.NewGuid()).Take(count).ToList();
            List<Course> courses = dbContext.Courses.ToList();
            List<PossibleChallengeVM> challenges = new List<PossibleChallengeVM>();

            for (int i = 0; i < count; i++)
            {
                Course course = courses[random.Next(0, courses.Count - 1)];
                User user = opponents[random.Next(0, opponents.Count - 1)];

                challenges.Add(new PossibleChallengeVM
                {
                    Course = course.Name,
                    CourseAvatar = fileService.GetDocument(dbContext.Documents
                    .FirstOrDefault(d => d.StreamId == course.StreamId)?.Path),
                    OpponentDescription = new UserShortDescriptionVM
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Avatar = fileService.GetDocument(dbContext.Documents
                    .FirstOrDefault(d => d.StreamId == user.ImageId)?.Path)
                    }
                });
            }

            return challenges;
        }

    }
}