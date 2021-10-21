using System.Collections.Generic;

namespace Programmania.ViewModels
{
    public class UserLessonsVM1
    {
        public string CurrentLessonName { get; set; }

        public Microsoft.AspNetCore.Html.HtmlString CurrentLessonHTML { get; set; }

        public IEnumerable<UserLessonVM1> Lessons { get; set; }

        public TestVM CurrentLessonTest { get; set; }
    }
}