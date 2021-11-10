namespace Programmania.ViewModels.Validators
{
    public class FileValidator
    {
        [Attributes.FileValidation(100000, ErrorMessage = "File size is too big")]
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
    }
}
