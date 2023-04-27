using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class ExpertHeader : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
