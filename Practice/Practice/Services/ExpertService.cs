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
        public async Task<IEnumerable<ExpertExpertPosition>> GetAll() => await _context.ExpertExpertPositions
                                                                                        .Include(e => e.Expert)
                                                                                        .Include(e => e.ExpertPosition)
                                                                                        .ToListAsync();

        public async Task<ExpertHeader> GetHeader()=> await  _context.ExpertHeaders.FirstOrDefaultAsync();

    }
}
