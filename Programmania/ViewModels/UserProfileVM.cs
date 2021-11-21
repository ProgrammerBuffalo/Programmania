using System;

namespace Programmania.ViewModels
{
    public class UserProfileVM
    {
        public string Nickname { get; set; }

        public string Email { get; set; }

        public bool IsOwned { get; set; }

        public int Expierence { get; set; }

        public int Level => (int)(Math.Sqrt(Expierence) / 150);

        public int ExpToNextLevelPercentage => Expierence * 100 / (int)Math.Pow(150 * (Level + 1), 2);

        public byte[] Avatar { get; set; }

        public UserChallengeStatsVM ChallengeStats { get; set; }

        public UserProfileVM(bool isOwned)
        {
            IsOwned = isOwned;
        }

    }
}
