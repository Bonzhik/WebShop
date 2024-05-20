
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text.RegularExpressions;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Helpers;
using WebShop.Logger;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICartRepository _cartRepository;
        private readonly ILogger _logger;
        private readonly string[] whiteList = [".jpg", ".png", ".gif", ".bmp"];

        public AccountsController(IAccountService accountService, ILogger<AccountsController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserW userW)
        {
            if (userW.Avatar != null)
            {
                if (!whiteList.Contains(Path.GetExtension(userW.Avatar.FileName)))
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

            string regex = @"(8){1}?[0-9]{3}?[0-9]{3}?[0-9]{2}?[0-9]{2}";

            if (!Regex.IsMatch(userW.PhoneNumber, regex))
            {
                return BadRequest("The phone number is uncorrect!");
            }

            var result = await _accountService.Register(userW);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetUserData")]
        public async Task<IActionResult> GetUserData()
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}");
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

        [Authorize]
        [HttpPost("ChangeUserInfo")]
        public async Task<IActionResult> ChangeUserInfo(DateTime birthDate, string surName, string name, string middleName)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user is trying to change the profile data!");
            string userName = User.Identity.Name;
            var result = await _accountService.ChangeUserInfo(birthDate, surName, name, middleName, userName);
            _logger.LogInformation($"The user received a response {result}");
            return Ok(result);
        }

        [Authorize]
        [HttpPost("ChangeUserAvatar")]
        public async Task<IActionResult> ChangeUserAvatar(IFormFile newAvatar)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user is trying to change the avatar!");
            if (newAvatar != null)
            {
                if (!whiteList.Contains(Path.GetExtension(newAvatar.FileName)))
                {
                    _logger.LogInformation("The file sent by the user had an incorrect format!");
                    return BadRequest("File has incorrect format!");
                }

                var result = await _accountService.ChangeAvatar(newAvatar, User.Identity.Name);

                _logger.LogInformation($"The user received a response {result}");

                return Ok(result);
            }
            else
            {
                _logger.LogInformation($"The user did not send the file!");
                return BadRequest("File is empty!");
            }
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user is trying to change the password!");
            string userName = User.Identity.Name;
            var result = await _accountService.ChangePassword(currentPassword, newPassword, userName);

            _logger.LogInformation($"The user received a response {result.Succeeded}");

            return Ok(result);
        }

        [Authorize]
        [HttpPost("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail(string newEmail, string password)
        {

            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");

            _logger.LogInformation("The user is trying to change the email address!");
            try
            {
                var email = new MailAddress(newEmail);
            }
            catch
            {
                _logger.LogInformation("The new email entered by the user turned out to be incorrect");
                return BadRequest("The mail is uncorrected!");
            }

            string userName = User.Identity.Name;
            var result = await _accountService.ChangeEmail(newEmail, userName, password);

            _logger.LogInformation($"The user received a response {result.Succeeded}");
            return Ok(result);

        }

        [Authorize]
        [HttpPost("ChangePhoneNumber")]
        public async Task<IActionResult> ChangePhoneNumber(string newPhoneNumber, string password)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user is trying to change the phone number!");
            string regex = @"(8){1}?[0-9]{3}?[0-9]{3}?[0-9]{2}?[0-9]{2}";


            if (!Regex.IsMatch(newPhoneNumber, regex))
            {
                _logger.LogInformation($"The phone number entered by the user turned out to be incorrect!");
                return BadRequest("The phone number is uncorrect!");
            }

            string userName = User.Identity.Name;
            var result = await _accountService.ChangePhoneNumber(newPhoneNumber, userName, password);

            _logger.LogInformation($"The user received a response {result.Succeeded}");

            return Ok(result);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddToRole")]
        public async Task<IActionResult> AddUserToRole(string userName, string roleName)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation($"The user is trying to add a {roleName} role to the user {userName}!");
            var result = await _accountService.AddUserToRole(userName, roleName);

            if (result.Succeeded)
            {
                _logger.LogInformation("The user added the user to the role!");
            }
            else
            {
                _logger.LogInformation($"The user was unable to add the user to the role! Description: {result.Succeeded}");
            }
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation($"User with username {username} try login");
            
            var result = await _accountService.Login(username, password, rememberMe);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User with username {username} authorized successfully!");
            }
            else
            {
                _logger.LogInformation($"The user with name {username} was unable to log in!");
            }

            return Ok(result);
        }

        [HttpGet("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}");
            _logger.LogInformation("The user logged out!");
            _accountService.Logout();
            return Ok();
        }
    }
}
