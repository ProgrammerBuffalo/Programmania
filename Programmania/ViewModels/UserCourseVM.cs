using System;

namespace Programmania.ViewModels
{
    public class UserCourseVM
    {
        public int CourseId { get; set; }

        public bool IsSelected { get; set; }

        public string CourseName { get; set; }

        public string Description { get; set; }

        public int LessonsCount { get; set; }

        public int LessonsCompleted { get; set; }

        public int Percentage => (int)Math.Round((double)(100 * LessonsCompleted) / LessonsCount);

        public byte[] Image { get; set; }

    }
}
