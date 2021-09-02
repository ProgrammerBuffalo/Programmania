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

        public Microsoft.AspNetCore.Http.IFormFile Avatar { get; set; }
    }
}
