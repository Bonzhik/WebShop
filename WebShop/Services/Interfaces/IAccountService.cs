using Microsoft.AspNetCore.Identity;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> Login(string username, string password, bool rememberMe);
        void Logout();
        void ChangePassword();
        void ChangeUsername();
        void ChangeEmail();
        Task<IdentityResult> Register(UserW userW);
    }
}
