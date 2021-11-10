using Programmania.Models;
using Programmania.ViewModels;
using System.Threading.Tasks;

namespace Programmania.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticationResponseVM> Authenticate(AuthenticationRequestVM authenticationRequest, string ipAdress);

        bool RefreshTokens(string refreshToken, string ipAdress, out string generatedJwtToken, out string generatedRToken, out User user);

        Task<bool> RevokeToken(string token, string ipAdress);

        bool ValidateJWTToken(string jwtToken, out int? claimUserId, out string claimUserlogin);

        User GetUserDBId(int? claimUserId, string claimUserlogin);

        void SetCookiesInApp(Microsoft.AspNetCore.Http.HttpResponse httpResponse, string jwtToken, string refreshToken);
    }
}
