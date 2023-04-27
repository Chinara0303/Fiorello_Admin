using Practice.Models;

namespace Practice.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
        Task<Product> GetFullDataByIdAsync(int id);
        Task<List<Product>> GetPaginatedDatasAsync(int page, int take);
        Task<List<Product>> GetAllAsync();
        Task<int> GetCountAsync();
    }
}
