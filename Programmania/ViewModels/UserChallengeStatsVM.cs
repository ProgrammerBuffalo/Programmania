using System;

namespace Programmania.ViewModels
{
    public class UserChallengeStatsVM
    {
        public int Wins { get; set; }

        public int Loses { get; set; }

        public int Draws { get; set; }

        public int GamesPlayed => Wins + Loses + Draws;

        public int Winrate => (int)Math.Round((double)(100 * Wins) / (Wins + Loses));
    }
}
