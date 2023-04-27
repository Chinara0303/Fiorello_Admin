using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Models
{
    public class SliderInfo:BaseEntity
    {
        [Required(ErrorMessage ="Don`t be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
        public string SignatureImage { get; set; }
        [NotMapped,Required(ErrorMessage = "Don`t be empty")]
        public IFormFile SignaturePhoto { get; set; }
    }
}
