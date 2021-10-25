using Microsoft.AspNetCore.Html;
using System.Text;

namespace Programmania.HtmlHelpers
{
    public static class QuestionHelper
    {
        public static HtmlString CreateQuestion(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html, ViewModels.TestVM test)
        {
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("<div class='content-tests'>");
            @string.AppendLine("<div class='content-tests-test' data-aos='fade-right' data-aos-duration='1000'>");

            @string.AppendLine($"<h3 class='content-tests__question text-normal'>{test.Question}</h3>");
            @string.AppendLine("<label class='rad-label'>");
            @string.AppendLine("<input type='radio' class='rad-input' name='rad' />");
            @string.AppendLine("<div class='rad-design'></div>");

            @string.AppendLine($"<div class='rad-text text-light'>{test.A1}</div>");
            @string.AppendLine("</label>");
            @string.AppendLine("<label class='rad-label'>");
            @string.AppendLine("<input type='radio' class='rad-input' name='rad' />");
            @string.AppendLine("<div class='rad-design'></div>");
            @string.AppendLine($"<div class='rad-text text-light'>{test.A2}</div>");

            @string.AppendLine("</label>");
            @string.AppendLine("<label class='rad-label'>");
            @string.AppendLine("<input type='radio' class='rad-input' name='rad' />");
            @string.AppendLine("<div class='rad-design'></div>");
            @string.AppendLine($"<div class='rad-text text-light'>{test.A3}</div>");
            @string.AppendLine("</label>");

            @string.AppendLine("<label class='rad-label'>");
            @string.AppendLine("<input type='radio' class='rad-input' name='rad' />");
            @string.AppendLine("<div class='rad-design'></div>");
            @string.AppendLine($"<div class='rad-text text-light'>{test.A4}</div>");
            @string.AppendLine("</label>");

            @string.AppendLine("</div>");
            @string.AppendLine("</div>");

            return new HtmlString(@string.ToString());
        }
    }
}