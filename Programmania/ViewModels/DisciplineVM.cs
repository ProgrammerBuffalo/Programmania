namespace Programmania.ViewModels
{
    public class DisciplineVM
    {
        public int DisciplineId { get; set; }

        public string DisciplineName { get; set; }

        public int LessonsCount { get; set; }

        public int LessonsCompleted { get; set; }

        public int Percentage => LessonsCompleted * 100 / LessonsCount;

        public byte[] Image { get; set; }
    }
}
