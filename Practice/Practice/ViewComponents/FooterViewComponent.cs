using Microsoft.AspNetCore.Mvc;
using Practice.Services.Interfaces;
using Practice.ViewModels.Footer;
using Practice.ViewModels.Layout;

namespace Practice.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly IFooterService _footerService;
        public FooterViewComponent(IFooterService footerService)
        {
            _footerService = footerService;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(new FooterVM {Socials  = await _footerService.GetAll(), 
                                                            Settings = _footerService.GetSettings()}));
        }
    }
}
