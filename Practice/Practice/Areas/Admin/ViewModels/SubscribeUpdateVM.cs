using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Areas.Admin.ViewModels
{
    public class SubscribeUpdateVM
    {
        public string Image { get; set; }

        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }
    }
}
