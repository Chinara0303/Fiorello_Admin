using Practice.Models;

namespace Practice.Services.Interfaces
{
    public interface IExpertService
    {
        Task<IEnumerable<ExpertExpertPosition>> GetAll();
        Task<ExpertHeader> GetHeader();
    }
}
