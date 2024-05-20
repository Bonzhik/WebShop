using AutoMapper;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Models;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebShop.Services.Implementations
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;
        public LogService(ILogRepository logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateLog(LogW log)
        {
            return await _logRepository.CreateLog(_mapper.Map<Log>(log));
        }

        public async Task<List<LogR>> GetAll()
        {
            List<Log> logs = await _logRepository.GetAll();
            List<LogR> result = new List<LogR>();
            foreach (var log in logs)
            {
                result.Add(_mapper.Map<LogR>(log));
            }
            return result;
        }

        public async Task<List<LogR>> GetByDate(DateTime date)
        {
            List<Log> logs = await _logRepository.GetByDate(date);
            List<LogR> result = new List<LogR>();
            foreach (var log in logs)
            {
                result.Add(_mapper.Map<LogR> (log));
            }
            return result;
        }

        public async Task<List<LogR>> GetByUserName(string userName)
        {
            List<Log> logs = await _logRepository.GetByUserName(userName);
            List<LogR> result = new List<LogR>();
            foreach (var log in logs)
            {
                result.Add(_mapper.Map<LogR>(log));
            }
            return result;
        }
    }
}
