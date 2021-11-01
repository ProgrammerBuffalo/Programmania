namespace Programmania.Attributes
{
    public class FileValidationAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        private readonly int maxFileSize;

        public FileValidationAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            Microsoft.AspNetCore.Http.IFormFile file = value as Microsoft.AspNetCore.Http.IFormFile;
            if (file != null)
            {
                if (file.Length > maxFileSize)
                    return false;
                else
                    return true;
            }
            return true;
        }
    }
}
