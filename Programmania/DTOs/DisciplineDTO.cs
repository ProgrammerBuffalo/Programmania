using Microsoft.AspNetCore.Http;

namespace Programmania.DTOs
{
    public class DisciplineDTO
    {
        public int CouseId { get; set; }
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public IFormFile Image { get; set; }
    }
}
