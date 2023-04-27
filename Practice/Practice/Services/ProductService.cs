using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;

namespace Practice.Services
{

    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.Images).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product> GetFullDataByIdAsync(int id)
        {
            return await _context.Products
                                 .Include(p => p.Images)
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<int> GetCountAsync() => await _context.Products.CountAsync();

        public async Task<List<Product>> GetPaginatedDatasAsync(int page,int take)
        {
            return await _context.Products
                    .Include(p => p.Images)
                    .Include(p => p.Category)
                    .Skip((page * take) - take)
                    .Take(take)
                    //.OrderByDescending(p=>p.CreateDate)
                    .ToListAsync();
        }
    }
}
