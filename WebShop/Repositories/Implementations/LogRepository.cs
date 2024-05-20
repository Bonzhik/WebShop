using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using WebShop.Data;
using WebShop.Models;
using WebShop.Repositories.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebShop.Repositories.Implementations
{
    public class LogRepository : ILogRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public LogRepository(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> SaveAsync(ApplicationContext _context)
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> CreateLog(Log log)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                try
                {
                    await _context.Logs.AddAsync(log);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                var result = await SaveAsync(_context);
                return result;
            }
            
            
        }

        public async Task<List<Log>> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                return await _context.Logs.ToListAsync();
            }
        }

        public async Task<List<Log>> GetByDate(DateTime date)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                return await _context.Logs.Where(p => p.Date.Date == date.Date).ToListAsync();
            }
        }

        public async Task<List<Log>> GetByUserName(string userName)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                return await _context.Logs.Where(p => p.UserName == userName).ToListAsync();
            }
        }
    }
}
