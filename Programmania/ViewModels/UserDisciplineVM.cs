using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.ViewModels
{
    public class UserDisciplineVM
    {
        public int DisciplineId { get; set; }

        public string DisciplineName { get; set; }

        public Dictionary<int, string> Lessons { get; set; }


    }
}
