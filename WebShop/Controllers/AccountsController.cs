
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Write;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(UserW userW)
        {
            var result = await _accountService.Register(userW);
            return Ok(result);
        }

        [HttpGet("/CheckUserProfile")]
        [Authorize]
        public IActionResult Index()
        {
            return Ok(User.Identity.Name + " " + User.Identity.AuthenticationType);
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe)
        {
            var result = await _accountService.Login(username, password, rememberMe);
            return Ok(result);
        }

        [HttpGet("/Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            _accountService.Logout();
            return Ok();
        }
    }
}
