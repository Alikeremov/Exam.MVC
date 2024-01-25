using Microsoft.EntityFrameworkCore;
using Pigga.Mvc.Exam.DAL;

namespace Pigga.Mvc.Exam.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string,string>> GetSettingAsync()
        {
            Dictionary<string,string> settings=await _context.Settings.ToDictionaryAsync(x=>x.Key,x=>x.Value);
            return settings;
        }
    }
}
