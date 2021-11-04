namespace Programmania.ViewModels.Validators
{
    public class NicknameValidator
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "nickname is required")]
        [System.ComponentModel.DataAnnotations.MinLength(5, ErrorMessage = "min length is 5")]
        [System.ComponentModel.DataAnnotations.MaxLength(8, ErrorMessage = "max length is 15")]
        public string Nickname { get; }
    }
}
