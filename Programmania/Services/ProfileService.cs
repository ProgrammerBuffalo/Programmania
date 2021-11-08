using Microsoft.AspNetCore.Http;
using Programmania.DAL;
using Programmania.Models;
using Programmania.ViewModels;
using Programmania.Services.Interfaces;
using System.Linq;

namespace Programmania.Services
{
    public class ProfileService : IProfileService
    {
        private ProgrammaniaDBContext dBContext;
        private IFileService fileService;

        public ProfileService(ProgrammaniaDBContext dBContext, IFileService fileService)
        {
            this.dBContext = dBContext;
            this.fileService = fileService;
        }

        public int ChangeAvatar(User user, IFormFile file)
        {
            var dbUser = dBContext.Users.FirstOrDefault(u => u.Id == user.Id && user.Login == u.Login);
            if (dbUser != null)
            {
                fileService.UpdateDocument(user.ImageId, file);
                dBContext.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int ChangeNickname(User user, string nickname)
        {
            var dbUser = dBContext.Users.FirstOrDefault(u => u.Id == user.Id && user.Login == u.Login);
            if (dbUser != null)
            {
                dbUser.Name = nickname;
                dBContext.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public UserProfileVM GetProfileData(User user)
        {
            if(user != null)
            {
                UserProfileVM profileVM = new UserProfileVM(true)
                {
                    Nickname = user.Name,
                    Avatar = fileService.GetSqlFileContext(user.ImageId)?.TransactionContext,
                    Email = user.Login,
                    Expierence = user.Exp,
                };
                return profileVM;
            }
            return null;
        }

        public UserProfileVM GetProfileData(int userId)
        {
            User user = dBContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                UserProfileVM profileVM = new UserProfileVM(true)
                {
                    Nickname = user.Name,
                    Avatar = fileService.GetSqlFileContext(user.ImageId)?.TransactionContext,
                    Expierence = user.Exp,
                };
                return profileVM;
            }
            return null;
        }        
    }
}
