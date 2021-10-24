using Microsoft.AspNetCore.Html;

namespace Programmania.ViewModels
{
    public class RequestedLessonVM
    {
        public HtmlString HTML { get; set; }

        public TestVM Test { get; set; }
    }
}
