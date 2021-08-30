using System.ComponentModel.DataAnnotations;

namespace Programmania.ViewModels
{
    public class AuthenticationRequestVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
