using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Models
{
    public class Subscribe: BaseEntity
    {
        public string Image { get; set; }

        [NotMapped,Required(ErrorMessage ="Don`t be empty")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }
    }
}
