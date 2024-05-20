using WebShop.Services.Interfaces;

namespace WebShop.Logger
{
    public class DbLoggerProvider : ILoggerProvider
    {

        private ILogService _logService;
        private IHttpContextAccessor _contextAccessor;

        public DbLoggerProvider(ILogService logService, IHttpContextAccessor contextAccessor)
        {
            _logService = logService;
            _contextAccessor = contextAccessor;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(_logService, _contextAccessor);
        }

        public void Dispose()
        {
        }
    }
}
