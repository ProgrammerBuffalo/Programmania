using Microsoft.AspNetCore.Http;

namespace Programmania.DTOs
{
    public class CourseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
