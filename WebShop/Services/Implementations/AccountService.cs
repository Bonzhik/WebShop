using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebShop.Dtos.Read;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;

        private readonly string avatarsDirectory = "UsersAvatars";

        public AccountService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            ICartRepository cartRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cartRepository = cartRepository;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Register(UserW userW)
        {
            User user = _mapper.Map<User>(userW);

            if (!Directory.Exists(avatarsDirectory))
            {
                Directory.CreateDirectory(avatarsDirectory);
            }

            if (userW.Avatar != null)
            {
                string fileName = userW.UserName + "-" + userW.Avatar.FileName;

                user.Avatar = fileName;
            }

            var result = await _userManager.CreateAsync(user, userW.password);

            if (result.Succeeded)
            {
                await _cartRepository.AddAsync(new Cart() { User = user });
                await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, "buyer");

                if (user.Avatar != null)
                {

                    string path = Path.Combine(avatarsDirectory, user.Avatar);


                    await using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await userW.Avatar.CopyToAsync(fileStream);
                    }
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

        public async Task<IdentityResult> ChangeEmail(string newEmail, string userName, string password)
        {
            User user = await _userManager.FindByNameAsync(userName);
            bool checkPassword = await _userManager.CheckPasswordAsync(user, password);
            if (checkPassword)
            {
                user.Email = newEmail;
                return await _userManager.UpdateAsync(user);
            }
            else
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "The password is not correct!"
                });
            }
        }

        public async Task<IdentityResult> ChangePhoneNumber(string newNumber, string userName, string password)
        {
            User user = await _userManager.FindByNameAsync(userName);
            bool result = await _userManager.CheckPasswordAsync(user, password);
            if (result)
            {
                user.PhoneNumber = newNumber;
                
                return await _userManager.UpdateAsync(user);
            }
            else
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "The password is not correct!"
                });
            }
        }

        public async Task<IdentityResult> ChangePassword(string currentPassword, string newPassword, string userName)
        {

            User user = await _userManager.FindByNameAsync(userName);
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        }

        public async Task<bool> ChangeAvatar(IFormFile avatar, string userName)
        {
            User user = await _userManager.FindByNameAsync(userName);

            string fileName = userName + "-" + avatar.FileName;

            string path = Path.Combine(avatarsDirectory, fileName);

            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
            }

            if (File.Exists(path))
            {
                user.Avatar = fileName;
                var result = await _userManager.UpdateAsync(user);
                return true;

            }

            return false;

        }

        public async Task<IdentityResult> ChangeUserInfo(DateTime birthDate, string surName, string name, string middleName, string userName)
        {
            User user = await _userManager.FindByNameAsync(userName);

            user.BirthDate = birthDate;
            user.SurName = surName;
            user.Name = name;
            user.MiddleName = middleName;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<UserR> GetUserData(string userName)
        {
            User user = await _userManager.FindByNameAsync(userName);
            return _mapper.Map<UserR>(user);
        }

        public string GetUserAvatarPath(string avatarName)
        {
            string directory = Path.GetFullPath(avatarsDirectory);
            string avatarPath = Path.Combine(directory, avatarName);

            if (!File.Exists(avatarPath))
            {
                avatarPath = null;
            }
            return avatarPath;
        }

        public async Task<IdentityResult> AddUserToRole(string userName, string roleName)
        {
            User user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "The user was not found!"
                });
            }

            ICollection<string> roles = await _userManager.GetRolesAsync(user);

            IdentityRole role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "The role was not found!"
                });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (roles != null && result.Succeeded)
            {
                await _userManager.RemoveFromRolesAsync(user, roles);
            }

            return result;
        }

    }
}
