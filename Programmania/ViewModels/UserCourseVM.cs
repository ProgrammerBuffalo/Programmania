using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.ViewModels
{
    public class UserCourseVM
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int LessonsCount { get; set; }

        public int LessonsCompleted { get; set; }

        public int Percentage => LessonsCompleted / LessonsCount * 100;

        public byte[] Image { get; set; }

    }
}
