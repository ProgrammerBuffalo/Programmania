using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.ViewModels
{
    public class UserLessonVM
    {
        public int LessonId { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsCompleted { get; set; }
    }
}
