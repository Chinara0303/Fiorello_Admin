using Practice.Models;
using System.ComponentModel.DataAnnotations;

namespace Practice.Areas.Admin.ViewModels
{
    public class ProductVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
