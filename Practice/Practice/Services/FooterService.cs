using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;

namespace Practice.Services
{
    public class FooterService : IFooterService
    {
        private readonly AppDbContext _context;
       
        public FooterService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Social>> GetAll() => await _context.Socials.ToListAsync();

        public Dictionary<string, string> GetSettings() => _context.Settings.AsEnumerable()
                                                                             .ToDictionary(s => s.Key, s => s.Value);
    }
}
