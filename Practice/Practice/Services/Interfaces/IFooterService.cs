using Practice.Models;

namespace Practice.Services.Interfaces
{
    public interface IFooterService
    {
        Task<IEnumerable<Social>> GetAll(); 
        Dictionary<string,string> GetSettings();

    }
}
