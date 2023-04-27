using Practice.Models;

namespace Practice.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAll();
        Task<BlogHeader> GetBlogHeader();
    }
}
