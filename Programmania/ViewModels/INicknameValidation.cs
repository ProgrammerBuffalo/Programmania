namespace Programmania.ViewModels
{
    public interface INicknameValidation
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "nickname is required")]
        [System.ComponentModel.DataAnnotations.MinLength(5, ErrorMessage = "min 5")]
        [System.ComponentModel.DataAnnotations.MaxLength(15, ErrorMessage = "max 15")]
        public string Nickname { get; }
    }

    public interface IFileValidation
    {
        [Attributes.FileValidation(100000, ErrorMessage = "")]
        public Microsoft.AspNetCore.Http.IFormFile File { get; }
    }
}
