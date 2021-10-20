using System.Collections.Generic;

namespace Programmania.ViewModels
{
    public class UserLessonsVM1
    {
        public Microsoft.AspNetCore.Html.HtmlString HTML { get; set; }

        public IEnumerable<UserLessonVM1> Lessons { get; set; }

        public TestVM Test { get; set; }
    }
}