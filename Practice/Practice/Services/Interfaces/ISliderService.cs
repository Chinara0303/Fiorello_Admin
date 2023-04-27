using Practice.Models;

namespace Practice.Services.Interfaces
{
    public interface ISliderService
    {
       Task<IEnumerable<Slider>> GetAll();
       Task<SliderInfo> GetInfo();
    }
}
