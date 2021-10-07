using Microsoft.AspNetCore.Html;

namespace Programmania.HtmlHelpers
{
    public static class DisciplineHelper
    {
        public static HtmlString CreateDiscipline(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, ViewModels.DisciplineVM discipline)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("<a data-aos='fade-right' data-aos-duration='1000'");
            builder.Append($"<div class='descipline' data-id='{discipline.DisciplineId}'>");
            builder.Append("<div class='subject'>");
            builder.Append($"<p class='subject-text'>{discipline.DisciplineName}</p>");
            builder.Append("</div>");
            builder.Append("<div class='subject__img'>");
            string image = System.Convert.ToBase64String(discipline.Image);
            image = string.Format("data:image/*;base64,{0}", image);
            builder.Append($"<img src='{image}' />");
            builder.Append("</div>");
            builder.Append("<div class='diagramm__body'>");
            builder.Append("<div class='ldBar label-center' data-preset='circle' data-value='25' data-transition-in='1000' style='width: 50%; height: 50%;'></div>");
            builder.Append("<div class='lesson-count'>");
            builder.Append($"<p class='lesson-count__text'>Lesson count {discipline.LessonsCompleted} / {discipline.LessonsCount}</p>");
            builder.Append("</div>");
            builder.Append("</div>");
            builder.Append("</div>");
            builder.Append("</a>");

            return new HtmlString(builder.ToString());
        }

        public static HtmlString CreatePassedDiscipline(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, ViewModels.DisciplineVM discipline)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("<a data-aos='fade-right' data-aos-duration='1000'");
            builder.Append($"<div class='descipline' dark-image data-id='{discipline.DisciplineId}'>");
            builder.Append("<div class='subject'>");
            builder.Append($"<p class='subject-text'>{discipline.DisciplineName}</p>");
            builder.Append("</div>");
            builder.Append("<div class='subject__img'>");
            string image = System.Convert.ToBase64String(discipline.Image);
            image = string.Format("data:image/*;base64,{0}", image);
            builder.Append($"<img src='{image}' />");
            builder.Append("</div>");
            builder.Append("<div class='diagramm__body'>");
            builder.Append("<div class='ldBar label-center' data-preset='circle' data-value='25' data-transition-in='1000' style='width: 50%; height: 50%;'></div>");
            builder.Append("<div class='lesson-count'>");
            builder.Append($"<p class='lesson-count__text'>Lesson count {discipline.LessonsCompleted} / {discipline.LessonsCount}</p>");
            builder.Append("</div>");
            builder.Append("</div>");
            builder.Append("</div>");
            builder.Append("</a>");

            return new HtmlString(builder.ToString());
        }
    }
}
