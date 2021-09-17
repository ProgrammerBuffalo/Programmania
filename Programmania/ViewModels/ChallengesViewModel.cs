namespace Programmania.ViewModels
{
    public class ChallengesViewModel
    {
        public int Games { get; set; }

        public int Wins { get; set; }

        public int Defeats { get; set; }

        public int Xp { get; set; }

        public Challange[] Challanges { get; set; }

        public ChallengesViewModel()
        {

        }
    }

    public enum WinState
    {
        Win,
        Loase,
        Draw
    }

    public enum Diffucult
    {
        Eazy,
        Medium,
        Hard
    }

    public class Challange
    {
        public Challange()
        {
            
        }

        public System.DateTime Date { get; set; }

        public int QuestionsCount { get; set; }

        public int AnsweredCount { get; set; }

        public int EarnedExp { get; set; }

        public Diffucult Diffucult { get; set; }

        public string Descipline { get; set; }
    }
}
