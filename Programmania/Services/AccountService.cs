using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Programmania.Models;
using Programmania.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Programmania.Services
{
    public class AccountService : IAccountService
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private readonly AppSettings appSettings;

        public AccountService(DAL.ProgrammaniaDBContext context, IOptions<AppSettings> app_settings)
        {
            this.dbContext = context;
            this.appSettings = app_settings.Value;
        }

        public async Task<AuthenticationResponseVM> Registrate(RegistrationVM registrationVM, string ipAdress)
        {
            User user = new User()
            {
                Name = registrationVM.Nickname,
                Password = registrationVM.Password,
                Login = registrationVM.Email
            };

            dbContext.Users.Add(user);

            string jwtToken = generateJwtToken(user);
            RefreshToken refreshToken = generateRefreshToken(ipAdress);

            user.RefreshTokens.Add(refreshToken);
            dbContext.Users.Add(user);

            await dbContext.SaveChangesAsync();

            return new AuthenticationResponseVM(jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticationResponseVM> Authenticate(AuthenticationRequestVM authenticationRequest, string ipAdress)
        {
            User user = dbContext.Users.FirstOrDefault(u => u.Login == authenticationRequest.Email && u.Password == authenticationRequest.Password);
            if (user == null) return null;

            string jwtToken = generateJwtToken(user);
            RefreshToken rt = generateRefreshToken(ipAdress);

            user.RefreshTokens.Add(rt);
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();

            return new AuthenticationResponseVM(jwtToken, rt.Token);
        }

        public async Task<AuthenticationResponseVM> RefreshTokens(string refreshToken, string ipAdress)
        {
            User user = dbContext.Users.Include(u => u.RefreshTokens).FirstOrDefault(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
            if (user == null) return null;

            var rt = user.RefreshTokens.First(t => t.Token == refreshToken);
            if (!rt.IsAlive) return null;

            var generatedRT = generateRefreshToken(ipAdress);
            rt.IpRevoker = ipAdress;
            rt.Revoked = DateTime.UtcNow;

            user.RefreshTokens.Add(generatedRT);

            dbContext.Update(user);
            await dbContext.SaveChangesAsync();

            var jwtToken = generateJwtToken(user);

            return new AuthenticationResponseVM(jwtToken, generatedRT.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipAdress)
        {
            var user = dbContext.Users.Include(u => u.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null) return false;

            var rt = user.RefreshTokens.First(t => t.Token == token);

            if (!rt.IsAlive) return false;

            rt.IpRevoker = ipAdress;
            rt.Revoked = DateTime.UtcNow;

            dbContext.Update(user);
            await dbContext.SaveChangesAsync();

            return true;
        }

        private RefreshToken generateRefreshToken(string ipAdress)
        {
            using (RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                byte[] generatedByServiceBytes = new byte[128];
                rngCryptoServiceProvider.GetBytes(generatedByServiceBytes);
                RefreshToken refreshToken = new RefreshToken()
                {
                    Token = Convert.ToBase64String(generatedByServiceBytes),
                    Created = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddDays(appSettings.RTExpirationDays),
                    IpCreator = ipAdress,
                    LocationCreator = "AZ" /*Utilities.LocationIdentifier.GetLocationByIP(ipAdress)*/
                };

                return refreshToken;
            }
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretCode);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
