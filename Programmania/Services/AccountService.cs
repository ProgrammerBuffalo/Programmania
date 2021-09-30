using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Programmania.Models;
using Programmania.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public bool RefreshTokens(string refreshToken, string ipAdress, out string generatedJwtToken, out string generatedRToken, out User user)
        {
            generatedJwtToken = null;
            generatedRToken = null;

            user = dbContext.Users.Include(u => u.RefreshTokens).FirstOrDefault(u => u.RefreshTokens.Any(t => t.Token == refreshToken));

            if (user == null) return false;

            var rt = user.RefreshTokens.First(t => t.Token == refreshToken);
            if (!rt.IsAlive) return false;

            var generatedRT = generateRefreshToken(ipAdress);
            rt.IpRevoker = ipAdress;
            rt.Revoked = DateTime.UtcNow;

            user.RefreshTokens.Add(generatedRT);

            dbContext.Update(user);
            dbContext.SaveChanges();

            generatedJwtToken = generateJwtToken(user);
            generatedRToken = generatedRT.Token;

            return true;
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

        public void SetCookiesInApp(HttpResponse httpResponse, string jwtToken, string refreshToken)
        {
            httpResponse.Cookies.Append("RefreshToken", refreshToken,
                new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(1),
                    HttpOnly = true
                });

            httpResponse.Cookies.Append("JwtToken", jwtToken,
                new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    HttpOnly = true
                });
        }

        public bool ValidateJWTToken(string jwtToken, out int? claimUserId, out string claimUserlogin)
        {
            claimUserId = null;
            claimUserlogin = null;

            if (jwtToken == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretCode);

            try
            {
                tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedJwtToken);

                JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)validatedJwtToken;

                claimUserId = int.Parse(jwtSecurityToken.Claims.First(c => c.Type == "nameid").Value);
                claimUserlogin = jwtSecurityToken.Claims.First(c => c.Type == "unique_name").Value;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public User GetUserDBId(int? claimUserId, string claimUserlogin)
        {
            if (claimUserId == null && claimUserlogin == null)
                return null;

            return dbContext.Users.FirstOrDefault(u => u.Id == claimUserId && u.Login == claimUserlogin);
        }
    }
}