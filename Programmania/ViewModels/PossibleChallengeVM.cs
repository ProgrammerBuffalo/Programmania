using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.ViewModels
{
    public class PossibleChallengeVM
    {
        public string Course { get; set; }

        public byte[] CourseAvatar { get; set; }

        public UserShortDescriptionVM OpponentDescription { get; set; }
    }
}
