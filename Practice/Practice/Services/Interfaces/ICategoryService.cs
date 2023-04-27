using Practice.Models;

namespace Practice.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<List<Category>> GetPaginatedDatas(int page, int take);
        Task<int> GetCountAsync();
    }
}
