using System.ComponentModel.DataAnnotations;

namespace Programmania.ViewModels
{
    public class AuthenticationRequestVM
    {
        [Required]
        [RegularExpression("^(([^<>()[\\]\\.,;:\\s@\"]+(\\.[^<>()[\\]\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^[a-z,A-Z,0-9,!,#,$,%,^,&]{8,16}$", ErrorMessage = "Password must be in range 8-18 and have only (!,#,$,%,^,&) symbols")]
        public string Password { get; set; }
    }
}
