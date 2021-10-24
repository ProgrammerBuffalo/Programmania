using Microsoft.AspNetCore.Html;
using System.Linq;
using System.Text;

namespace Programmania.HtmlHelpers
{
    public static class LessonHelper
    {
        public static HtmlString CreateLessonsMenu(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html,
            System.Collections.Generic.IEnumerable<ViewModels.UserLessonVM> lessons)
        {
            if (lessons != null)
            {
                var orderedLessons = lessons.OrderBy(l => l.Order);
                StringBuilder @string = new StringBuilder();

                foreach (var lesson in orderedLessons)
                {
                    if (lesson.IsCompleted)
                        @string.AppendLine($"<div class='burger-content-elem burger-content-elem_read' data-id='{lesson.Id}'>");
                    else
                        @string.AppendLine($"<div class='burger-content-elem burger-content-elem_unread' data-id='{lesson.Id}'>");
                    @string.AppendLine("<h3 class='burger-content-elem__text text-normal'>");
                    @string.AppendLine($"#{lesson.Order + 1} {lesson.Name}");
                    @string.AppendLine($"</h3>");
                    @string.AppendLine("</div>");
                }

                return new HtmlString(@string.ToString());
            }
            return null;
        }
    }
}