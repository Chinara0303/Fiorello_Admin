using Microsoft.AspNetCore.Mvc;
using Practice.Services.Interfaces;
using Practice.ViewModels.Expert;

namespace Practice.ViewComponents
{
    public class ExpertViewComponent : ViewComponent
    {
        private readonly IExpertService _expertService;
        public ExpertViewComponent(IExpertService expertService)
        {
            _expertService = expertService;
        }
        public async Task<IViewComponentResult> InvokeAsync() => await Task.FromResult(View(new ExpertVM {
                                                             ExpertExpertPositions = await _expertService.GetAll(),
                                                             ExpertHeader = await _expertService.GetHeader()}));

    }
}
