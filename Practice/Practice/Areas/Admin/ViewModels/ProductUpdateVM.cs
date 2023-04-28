using Practice.Models;
using System.ComponentModel.DataAnnotations;

namespace Practice.Areas.Admin.ViewModels
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public ICollection<ProductImage> Images { get; set; }

        public List<IFormFile> Photos { get; set; }
    }
}
