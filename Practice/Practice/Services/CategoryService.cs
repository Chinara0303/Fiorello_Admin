using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;

namespace Practice.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAll() => await _context.Categories.ToListAsync();

        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<List<Category>> GetPaginatedDatas(int page, int take)
        {
            return await _context.Categories.Skip((page * take) - take).Take(take).ToListAsync();
        }
    }
}
