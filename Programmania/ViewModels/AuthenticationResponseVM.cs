using System.Text.Json.Serialization;

namespace Programmania.ViewModels
{
    public class AuthenticationResponseVM
    {
        public string JWTToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public AuthenticationResponseVM(string jwtToken, string refreshToken)
        {
            JWTToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
