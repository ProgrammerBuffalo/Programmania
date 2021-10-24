using Newtonsoft.Json;
using System;

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
