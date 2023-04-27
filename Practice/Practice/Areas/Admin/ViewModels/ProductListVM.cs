using Practice.Models;

namespace Practice.Areas.Admin.ViewModels
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
    }
}
