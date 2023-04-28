using Practice.Models;
using System.Collections;

namespace Practice.Services.Interfaces
{
    public interface IExpertService
    {
        Task<List<Expert>> GetAll();
        Task<List<ExpertExpertPosition>> GetAllForExpertExpertPositions();
        Task<ExpertHeader> GetHeader();
    }
}
