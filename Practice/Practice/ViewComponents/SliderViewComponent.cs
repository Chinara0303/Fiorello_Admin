using Microsoft.AspNetCore.Mvc;
using Practice.Services.Interfaces;
using Practice.ViewModels.Slider;

namespace Practice.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly ISliderService _sliderService;
        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync() => await Task.FromResult(View(new SliderVM {
                                                                Sliders = await _sliderService.GetAll(),
                                                                SliderInfo = await _sliderService.GetInfo() }));
    }
}
