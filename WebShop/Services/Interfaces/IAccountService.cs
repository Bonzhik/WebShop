using Microsoft.AspNetCore.Identity;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> Login(string username, string password, bool rememberMe);
        void Logout();
        Task<IdentityResult> ChangePassword(string currentPassword, string newPassword, string userName);
        Task<IdentityResult> ChangeEmail(string newEmail, string userName, string password);
        Task<IdentityResult> ChangePhoneNumber(string newNumber, string userName, string password);
        Task<IdentityResult> Register(UserW userW);
        Task<bool> ChangeAvatar(IFormFile avatar, string userName);
        Task<UserR> GetUserData(string userName);
        string GetUserAvatarPath(string avatarName);

        Task<IdentityResult> ChangeUserInfo(DateTime birthDate, string surName, string name, string middleName, string userName);
        Task<IdentityResult> AddUserToRole(string userName, string roleName);
    }
}
