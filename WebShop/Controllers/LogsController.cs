using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("GetLogs")]
        public async Task<List<LogR>> getAll()
        {
            return await _logService.GetAll();
        }

        [HttpGet("GetLogsByUsername")]
        public async Task<List<LogR>> getByUser(string username)
        {
            return await _logService.GetByUserName(username);
        }

        [HttpGet("GetLogsByDate")]
        public async Task<List<LogR>> getByDate(DateTime date)
        {
            return await _logService.GetByDate(date);
        }
    }
}
