using System.ComponentModel.DataAnnotations;

namespace Programmania.ViewModels
{
    public class RegistrationVM
    {
        public string Nickname { get; set; }

        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }

        public Microsoft.AspNetCore.Http.IFormFile FormFile { get; set; }
    }
}
