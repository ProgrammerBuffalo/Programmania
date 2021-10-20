using Microsoft.AspNetCore.Html;

namespace Programmania.HtmlHelpers
{
    public static class CourseHelper
    {
        public static HtmlString CreateCourse(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, ViewModels.UserCourseVM course)
        {
            System.Text.StringBuilder @string = new System.Text.StringBuilder();

            //@string.AppendLine("<a>");
            if (course.IsSelected)
                @string.AppendLine($"<div class='course' data-id='{course.CourseId}'>");
            else
                @string.AppendLine($"<div class='course course_selected' data-id='{course.CourseId}'>");
            @string.AppendLine("<div class='course__image'>");
            string base64String = System.Convert.ToBase64String(course.Image, 0, course.Image.Length);
            @string.AppendLine($"<img src='data:image/*;base64,{base64String}' />");
            @string.AppendLine("</div>");
            @string.AppendLine("<div class='course-content'>");
            @string.AppendLine($"<h3 class='course__title'>{course.CourseName}</h3>");
            @string.AppendLine($"<p class='course__info'>Lesson count: {course.LessonsCompleted}/{course.LessonsCount}</p>");
            @string.AppendLine("<div class='course__percent'>");
            @string.AppendLine($"<div class='ldBar label-center' data-preset='circle' data-value='{course.Percentage}' data-transition-in='1000' style='width:100%; height:100%;'></div>");
            @string.AppendLine("</div>");
            @string.AppendLine("</div>");
            @string.AppendLine("</div>");
            //@string.AppendLine("</a>");

            return new HtmlString(@string.ToString());
        }
    }
}
