using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
