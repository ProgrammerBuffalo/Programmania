using System.Threading.Tasks;
using Programmania.Models;
using Programmania.ViewModels;

namespace Programmania.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponseVM> Authenticate(AuthenticationRequestVM authenticationRequest, string ipAdress);

        Task<AuthenticationResponseVM> RefreshTokens(string refreshToken, string ipAdress);
        
        Task<bool> RevokeToken(string token, string ipAdress);
    }
}
