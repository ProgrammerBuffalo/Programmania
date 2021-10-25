using Microsoft.AspNetCore.Html;
using System.Text;

namespace Programmania.HtmlHelpers
{
    public static class LessonHelper
    {
        public static HtmlString CreateLesson(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, ViewModels.UserLessonVM1 lesson, int order)
        {
            StringBuilder @string = new StringBuilder();
            if (lesson.IsCompleted)
                @string.AppendLine($"<div class='burger-content-elem burger-content-elem_read' data-id='{{\"lessonId\": \"{lesson.Id}\", \"streamId\": \"{lesson.StreamId}\"}}'>");
            else
                @string.AppendLine($"<div class='burger-content-elem burger-content-elem_unread' data-id='{{\"lessonId\": \"{lesson.Id}\", \"streamId\": \"{lesson.StreamId}\"}}'>");
            @string.AppendLine("<h3 class='burger-content-elem__text text-normal'>");
            @string.AppendLine($"#{order + 1} {lesson.Name}");
            @string.AppendLine($"</h3>");
            @string.AppendLine("</div>");

            return new HtmlString(@string.ToString());
        }
    }
}
