using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.ViewModels
{
    public class ChallengeVM
    {
        public int Id { get; set; }

        public string Course { get; set; }

        public DateTime Date { get; set; }

        public UserShortDescriptionVM OpponentDescription { get; set; }
    }
}
