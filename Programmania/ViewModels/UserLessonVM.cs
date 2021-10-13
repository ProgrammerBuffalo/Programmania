using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.ViewModels
{
    public class UserLessonVM
    {
        [JsonIgnore]
        public Guid StreamId { get; set; }

        public int LessonId { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public string HTML { get; set; }

        public bool IsCompleted { get; set; }

        public TestVM Test { get; set; }
    }
}
