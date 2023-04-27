using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Models
{
    public class Slider:BaseEntity
    {
        public string Image { get; set; }
        [NotMapped,Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }
    }
}
