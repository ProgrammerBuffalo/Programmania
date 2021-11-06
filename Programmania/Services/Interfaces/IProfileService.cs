namespace Programmania.Services.Interfaces
{
    public interface IProfileService
    {
        public int ChangeAvatar(Models.User user, Microsoft.AspNetCore.Http.IFormFile file);

        public int ChangeNickname(Models.User user, string nickname);

        public ViewModels.UserProfileVM GetProfileData(Models.User user);

        public ViewModels.UserProfileVM GetProfileData(int userId);
    }
}
