
using Microsoft.AspNetCore.Mvc;
using Practice.Services;
using Practice.Services.Interfaces;
using Practice.ViewModels.Blog;

namespace Practice.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public BlogViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }
   
        public async Task<IViewComponentResult> InvokeAsync() => await Task.FromResult(View(new BlogVM
        {
            Blogs = await _blogService.GetAll(),
            BlogHeader = await _blogService.GetBlogHeader()
        }));
    }
}
