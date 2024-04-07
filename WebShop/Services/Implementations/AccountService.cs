using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using WebShop.Dtos.Write;
using WebShop.Models;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;

namespace WebShop.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ICartRepository _cartRepository;

        private readonly string avatarsDirectory = "UsersAvatars";

        public AccountService(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ICartRepository cartRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cartRepository = cartRepository;   
        }

        public async Task<IdentityResult> Register(UserW userW)
        {
            User user = MapFromDto(userW);

            if (!Directory.Exists(avatarsDirectory)){
                Directory.CreateDirectory(avatarsDirectory);
            }
            string fileName = userW.UserName + userW.avatar.FileName;

            user.Avatar = fileName;
            var result = await _userManager.CreateAsync(user, userW.password);

            if (result.Succeeded)
            {
                await _cartRepository.AddAsync(new Cart() { User = user });
                await _signInManager.SignInAsync(user, false);

                string path = Path.Combine(avatarsDirectory, fileName);


                await using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await userW.avatar.CopyToAsync(fileStream);
                }
            }

            return result;
        }


        public async Task<SignInResult> Login(string username, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, rememberMe, false);
            return result;
        }

        public async void Logout()
        {
            await _signInManager.SignOutAsync(); ;
        }

        public void ChangeUsername()
        {
            throw new NotImplementedException();
        }

        public void ChangeEmail()
        {
            throw new NotImplementedException();
        }

        public void ChangePassword()
        {
            throw new NotImplementedException();
        }

        public User MapFromDto(UserW userW)
        {
            User user = new User
            {
                UserName = userW.UserName,
                Name = userW.Name,
                MiddleName = userW.MiddleName,
                SurName = userW.SurName,
                BirthDate = userW.BirthDate,
                PhoneNumber = userW.PhoneNumber,
                Email = userW.Email,
            };

            return user;
        }

    }
}
