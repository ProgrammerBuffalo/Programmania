using Microsoft.AspNetCore.Html;
using System.Text;

namespace Programmania.HtmlHelpers
{
    public static class QuestionHelper
    {
        public static HtmlString CreateTest(this Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper html,
            ViewModels.TestVM test)
        {
            if (test != null)
            {
                StringBuilder @string = new StringBuilder();
                //@string.AppendLine("<div class='content-tests-test' data-aos='fade-right' data-aos-duration='1000'>");
                @string.AppendLine($"<h3 class='content-tests__question text-normal'>{test.Question}</h3>");

                @string.Append(CreateAnswer(test.A1));
                @string.Append(CreateAnswer(test.A2));
                @string.Append(CreateAnswer(test.A3));
                @string.Append(CreateAnswer(test.A4));

                //@string.AppendLine("</div>");

                return new HtmlString(@string.ToString());
            }
            else
                return null;
        }

        private static StringBuilder CreateAnswer(string answer)
        {
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("<label class='rad-label'>");
            @string.AppendLine("<input type='radio' class='rad-input' name='rad' />");
            @string.AppendLine("<div class='rad-design'></div>");
            @string.AppendLine($"<div class='rad-text text-light'>{answer}</div>");
            @string.AppendLine("</label>");
            return @string;
        }
    }
}