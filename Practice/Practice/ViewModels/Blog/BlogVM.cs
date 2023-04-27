using Practice.Models;

namespace Practice.ViewModels.Blog
{
    public class BlogVM
    {
        public IEnumerable<Practice.Models.Blog> Blogs { get; set; }
        public BlogHeader BlogHeader { get; set; }
    }
}
