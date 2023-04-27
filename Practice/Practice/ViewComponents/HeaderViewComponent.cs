using Microsoft.AspNetCore.Mvc;
using Practice.Services.Interfaces;

namespace Practice.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async  Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(_layoutService.GetSettingDatas()));
        }
    }
}
