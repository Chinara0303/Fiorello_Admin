using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
