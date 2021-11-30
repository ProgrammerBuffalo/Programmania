namespace Programmania.ViewModels
{
    public class PossibleChallengeVM
    {
        public int CourseId { get; set; }

        public string Course { get; set; }

        public byte[] CourseAvatar { get; set; }

        public UserShortDescriptionVM OpponentDescription { get; set; }
    }
}
