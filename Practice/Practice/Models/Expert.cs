﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Models
{
    public class Expert : BaseEntity
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        public string Image { get; set; }

        [NotMapped,Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }
        public List<ExpertExpertPosition> ExpertExpertPosition { get; set; }
        [NotMapped]
        public List<int> ExpertPostionIds { get; set; }
    }
}
