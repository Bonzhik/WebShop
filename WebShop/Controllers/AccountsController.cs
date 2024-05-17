
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text.RegularExpressions;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Helpers;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly string[] whiteList = [".jpg", ".png", ".gif", ".bmp"];

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserW userW)
        {
            if (userW.avatar != null)
            {
                if (!whiteList.Contains(Path.GetExtension(userW.avatar.FileName)))
                {
                    return BadRequest("File has incorrect format!");
                }
            }

            try
            {
                var email = new MailAddress(userW.Email);
            }
            catch
            {
                return BadRequest("The mail is uncorrected!");
            }

            string regex = @"(8|\+7){1}?[-. ]?[0-9]{3}?[-. ]?[0-9]{3}?[-. ]?[0-9]{2}?[-. ]?[0-9]{2}";

            if (!Regex.IsMatch(userW.PhoneNumber, regex))
            {
                return BadRequest("The phone number is uncorrect!");
            }

            var result = await _accountService.Register(userW);
            return Ok(result);
        }

        [HttpGet("GetUserData")]
        [Authorize]
        public async Task<IActionResult> getUserData()
        {
            UserR userR = await _accountService.GetUserData(User.Identity.Name);
            return Ok(userR);
        }

        [HttpGet("GetUserAvatar")]
        public async Task<IActionResult> GetUserAvatar(string avatarName)
        {
            string avatarPath = _accountService.GetUserAvatarPath(avatarName);

            if (avatarPath == null)
            {
                return Ok("File not found!");
            }
            return PhysicalFile(avatarPath, MimeMapper.mappings[Path.GetExtension(avatarName)], avatarName);
        }

        [HttpPost("ChangeUserInfo")]
        public async Task<IActionResult> ChangeUserInfo(DateTime birthDate, string surName, string name, string middleName)
        {
            string userName = User.Identity.Name;
            var result = await _accountService.ChangeUserInfo(birthDate, surName, name, middleName, userName);
            return Ok(result);
        }

        [HttpPost("ChangeUserAvatar")]
        public async Task<IActionResult> ChangeUserAvatar(IFormFile newAvatar)
        {
            if (newAvatar != null)
            {
                if (!whiteList.Contains(Path.GetExtension(newAvatar.FileName)))
                {
                    return BadRequest("File has incorrect format!");
                }

                var result = await _accountService.ChangeAvatar(newAvatar, User.Identity.Name);
                return Ok(result);
            }
            else
            {
                return BadRequest("File is empty!");
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            string userName = User.Identity.Name;
            var result = await _accountService.ChangePassword(currentPassword, newPassword, userName);
            return Ok(result);
        }

        [HttpPost("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail(string newEmail, string password)
        {
            try
            {
                var email = new MailAddress(newEmail);
            }
            catch
            {
                return BadRequest("The mail is uncorrected!");
            }

            string userName = User.Identity.Name;
            var result = await _accountService.ChangeEmail(newEmail, userName, password);


            return Ok(result);

        }

        [HttpPost("ChangePhoneNumber")]
        public async Task<IActionResult> ChangePhoneNumber(string newPhoneNumber, string password)
        {
            string regex = @"(8|\+7){1}?[-. ]?[0-9]{3}?[-. ]?[0-9]{3}?[-. ]?[0-9]{2}?[-. ]?[0-9]{2}";

            if (!Regex.IsMatch(newPhoneNumber, regex))
            {
                return BadRequest("The phone number is uncorrect!");
            }

            string userName = User.Identity.Name;
            var result = await _accountService.ChangePhoneNumber(newPhoneNumber, userName, password);


            return Ok(result);

        }

        [HttpPost("AddToRole")]
        public async Task<IActionResult> AddUserToRole(string userName, string roleName)
        {
            var result = await _accountService.AddUserToRole(userName, roleName);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe)
        {
            var result = await _accountService.Login(username, password, rememberMe);
            return Ok(result);
        }

        [HttpGet("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            _accountService.Logout();
            return Ok();
        }
    }
}
