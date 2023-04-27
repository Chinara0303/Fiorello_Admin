using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;

namespace Practice.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<BlogHeader> GetBlogHeader()
        {
            return await _context.BlogHeaders.FirstOrDefaultAsync();
        }
    }
}
