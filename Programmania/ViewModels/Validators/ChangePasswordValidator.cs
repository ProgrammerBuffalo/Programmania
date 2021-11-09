using System.ComponentModel.DataAnnotations;

namespace Programmania.ViewModels.Validators
{
    public class ChangePasswordValidator
    {
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^[a-z,A-Z,0-9,!,#,$,%,^,&]{8,16}$", ErrorMessage = "Password must be in range 8-18 and have only (!,#,$,%,^,&) symbols")]
        public string NewPassword { get; set; }
    }
}
