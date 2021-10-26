using System;

namespace Programmania.ViewModels
{
    public class UserDisciplineVM
    {
        public int DisciplineId { get; set; }

        public string DisciplineName { get; set; }

        public int LessonsCount { get; set; }

        public int LessonsCompleted { get; set; }

        public int Percentage => (int)Math.Round((double)(100 * LessonsCompleted) / LessonsCount);

        public byte[] Image { get; set; }

    }
}
