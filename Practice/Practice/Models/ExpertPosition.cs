using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Models
{
    public class ExpertPosition : BaseEntity
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        public List<ExpertExpertPosition> ExpertExpertPositions { get; set; }
        [NotMapped]
        public List<int> ExpertIds { get; set; }

    }
}
