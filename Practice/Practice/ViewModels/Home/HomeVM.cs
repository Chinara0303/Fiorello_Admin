using Practice.Models;

namespace Practice.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Instagram> Instagrams { get; set; }
        public About About { get; set; }
        public Subscribe Subscribe { get; set; }
        public BlogHeader BlogHeader { get; set; }

    }
}
