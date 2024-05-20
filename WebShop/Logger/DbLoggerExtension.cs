using WebShop.Services.Interfaces;

namespace WebShop.Logger
{
    public static class DbLoggerExtension
    {
        public static ILoggingBuilder AddDbLogging(this ILoggingBuilder builder, ILogService logService, IHttpContextAccessor contextAccessor)
        {
            builder.AddProvider(new DbLoggerProvider(logService, contextAccessor));
            return builder;
        }
    }
}
