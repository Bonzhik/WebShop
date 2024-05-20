using Microsoft.AspNetCore.HttpLogging;
using WebShop.Data;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Services.Interfaces;

namespace WebShop.Logger
{
    public class DbLogger : ILogger, IDisposable
    {
        private readonly ILogService _logService;
        private readonly IHttpContextAccessor _contextAccessor;
        public DbLogger(ILogService logService, IHttpContextAccessor accessor)
        {
            _logService = logService;
            _contextAccessor = accessor;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }

        public void Dispose(){ }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            
            LogW logW = new LogW
            {
                Status = logLevel.ToString(),
                Message = formatter(state, exception),
                UserName = formatter.Method.Attributes.ToString()
            };

            if (_contextAccessor.HttpContext != null)
            {
                logW.UserName = _contextAccessor.HttpContext.User.Identity.Name;
                if (logW.UserName == null)
                {
                    logW.UserName = "Unauthorized";
                }
            }

            var result = await _logService.CreateLog(logW);

            if (!result)
            {
                Console.WriteLine("Failed to save the log! " + logW.UserName + ": " + logW.Message);
            }
            
        }

    }
}
