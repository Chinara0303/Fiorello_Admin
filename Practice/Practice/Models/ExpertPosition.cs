using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class ExpertPosition : BaseEntity
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        public IEnumerable<ExpertExpertPosition> ExpertPositions { get; set; } 
    }
}
