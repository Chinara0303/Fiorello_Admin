using Practice.Models;
using System.ComponentModel.DataAnnotations;

namespace Practice.Areas.Admin.ViewModels
{
    public class ProductUpdateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        
        public List<IFormFile> Photos { get; set; }
        public ICollection<ProductImage> Images { get; set; }
    }
}
