using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;

namespace Practice.Services
{
    public class ExpertService : IExpertService
    {
        private readonly AppDbContext _context;
        public ExpertService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Expert>> GetAll() => await _context.Experts
                                                                    .Include(e => e.ExpertExpertPosition)
                                                                    .ThenInclude(ep => ep.ExpertPosition)
                                                                    .ToListAsync();

        public async Task<List<ExpertExpertPosition>> GetAllForExpertExpertPositions()
        {
           return await _context.ExpertExpertPositions.Include(ep=>ep.Expert).Include(ep => ep.ExpertPosition).ToListAsync();
        }

        public async Task<ExpertHeader> GetHeader()=> await  _context.ExpertHeaders.FirstOrDefaultAsync();

    }
}
