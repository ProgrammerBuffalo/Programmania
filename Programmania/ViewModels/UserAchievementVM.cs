using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.ViewModels
{
    public class UserAchievementVM
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Points { get; set; }

        public byte[] Image { get; set; }
    }
}
