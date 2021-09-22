using System.ComponentModel.DataAnnotations;

namespace Programmania.ViewModels
{
    public class RegistrationVM
    {
        [Required]
        [Range(5, 20, ErrorMessage = "Please enter nickname in range 5-20 symbols")]
        public string Nickname { get; set; }

        [Required]
        [RegularExpression("^(([^<>()[\\]\\.,;:\\s@\"]+(\\.[^<>()[\\]\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        //[DataType(DataType.Password)]
        [Required]
        [RegularExpression("^[a-z,A-Z,0-9,!,#,$,%,^,&]{8,16}$", ErrorMessage = "Password must be in range 8-18 and have only (!,#,$,%,^,&) symbols")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "Password confirmation does not match")]
        public string PasswordConfirmation { get; set; }

        public Microsoft.AspNetCore.Http.IFormFile FormFile { get; set; }
    }
}
