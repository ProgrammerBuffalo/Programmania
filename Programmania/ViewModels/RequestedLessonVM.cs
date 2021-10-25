using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Programmania.ViewModels
{
    public class RequestedLessonVM
    {
        public Microsoft.AspNetCore.Html.HtmlString HTML { get; set; }

        public TestVM Test { get; set; }
    }
}
