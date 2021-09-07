using System;
using System.Collections.Generic;

namespace Programmania.ViewModels
{
    public class UserProfileVM
    {
        public string Nickname { get; set; }

        public string Email { get; set; }

        public bool IsOwned { get; set; }

        public int Expierence { get; set; }

        public int Level => (int)(Math.Sqrt(Expierence) / 50);

        public byte[] Avatar { get; set; }

        public ICollection<UserCourseVM> UserCourses { get; set; }

        public ICollection<UserAchievementVM> UserAchievements { get; set; }

        public UserProfileVM(bool isOwned)
        {
            IsOwned = isOwned;
        }

        public UserProfileVM(bool isOwned, string email, string name, int exp, ICollection<UserCourseVM> userCourses,
            ICollection<UserAchievementVM> userAchievements) : this(isOwned)
        {
            Email = email; Nickname = name; Expierence = exp;
            UserCourses = userCourses;
            UserAchievements = userAchievements;
        }

    }
}
