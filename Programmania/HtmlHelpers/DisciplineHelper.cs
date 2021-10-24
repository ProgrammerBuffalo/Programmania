using Microsoft.AspNetCore.Html;

namespace Programmania.HtmlHelpers
{
    public static class DisciplineHelper
    {
        public static HtmlString CreateDiscipline(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html,
            ViewModels.UserDisciplineVM discipline)
        {
            System.Text.StringBuilder @string = new System.Text.StringBuilder();

            //@string.AppendLine("<a>");
            if (discipline.LessonsCompleted > 0 && discipline.LessonsCompleted != discipline.LessonsCount)
                @string.AppendLine($"<div class='discipline' data-id='{discipline.DisciplineId}'>");
            else
                @string.AppendLine($"<div class='discipline discipline_selected' data-id='{discipline.DisciplineId}'>");
            @string.AppendLine("<div class='discipline__image'>");
            string base64String = System.Convert.ToBase64String(discipline.Image, 0, discipline.Image.Length);
            @string.AppendLine($"<img src='data:image/*;base64,{base64String}' />");
            @string.AppendLine("</div>");
            @string.AppendLine("<div class='discipline-content'>");
            @string.AppendLine($"<h3 class='discipline__title'>{discipline.DisciplineName}</h3>");
            @string.AppendLine($"<p class='discipline__info'>Lesson count: {discipline.LessonsCompleted}/{discipline.LessonsCount}</p>");
            @string.AppendLine("<div class='discipline__percent'>");
            @string.AppendLine($"<div class='ldBar label-center' data-preset='circle' data-value='{discipline.Percentage}' data-transition-in='1000' style='width: 100 %; height: 100 %;'></div>");
            @string.AppendLine("</div>");
            @string.AppendLine("</div>");
            @string.AppendLine("</div>");
            //@string.AppendLine("</a>");

            return new HtmlString(@string.ToString());
        }
    }
}
