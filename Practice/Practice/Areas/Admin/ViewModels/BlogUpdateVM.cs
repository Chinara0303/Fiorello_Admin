using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Areas.Admin.ViewModels
{
    public class BlogUpdateVM
    {
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage ="Don`t be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public DateTime Date { get; set; }
    }
}
