using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Programmania.TagHelpers
{
    public static class CourseHelper
    {
        public static HtmlString GetUserCourse(this IHtmlHelper tags, ViewModels.UserCourseVM course)
        {
            System.Text.StringBuilder @string = new System.Text.StringBuilder();
            @string.AppendLine("<a>");
            @string.AppendLine("<div class='course'>");
            @string.AppendLine("<div class='course__image lifted'>");
            string base64String = System.Convert.ToBase64String(course.Image, 0, course.Image.Length);
            @string.AppendLine($"<img src = 'data:image/*;base64,{base64String}' />");
            @string.AppendLine("</div>");
            @string.AppendLine("<div class='course-body'>");
            @string.AppendLine("<div class='course-info'>");
            @string.AppendLine($"<h5>{course.CourseName}</h5>");
            @string.AppendLine($"<p> Lessons Count: {course.LessonsCompleted} / {course.LessonsCount}</p>");
            @string.AppendLine("</div>");
            @string.AppendLine($"<div class='ldBar label-center' data-preset='circle' data-value='{course.Percentage}' data-transition-in='1000' style='width: 50%; height: 50%;'></div>");
            @string.AppendLine("</div>");
            @string.AppendLine("</div>");
            @string.AppendLine("</a>");
            return new HtmlString(@string.ToString());
        }

        public static HtmlString GetCourse(this IHtmlHelper tags, ViewModels.UserCourseVM course)
        {
            System.Text.StringBuilder @string = new System.Text.StringBuilder();
            @string.AppendLine("<a>");
            @string.AppendLine("<div class='course'>");
            @string.AppendLine("<div class='course__image lifted dark-image'>");
            string base64String = System.Convert.ToBase64String(course.Image, 0, course.Image.Length);
            @string.AppendLine($"<img src='data:image/*;base64,{base64String}' />");
            @string.AppendLine("</div>");
            @string.AppendLine("<div class='course-body'>");
            @string.AppendLine("<div class='course-info'>");
            @string.AppendLine($"<h5>{course.CourseName}</h5>");
            @string.AppendLine($"<p> Lessons Count: {course.LessonsCompleted} / {course.LessonsCount}</p>");
            @string.AppendLine("</div>");
            @string.AppendLine($"<div class='course-info__text'>{course.Description}</div>");
            @string.AppendLine("</div>");
            @string.AppendLine("</div>");
            @string.AppendLine("</a>");
            return new HtmlString(@string.ToString());
        }
    }
}