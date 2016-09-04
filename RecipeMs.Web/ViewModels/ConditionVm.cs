using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeMs.Web.ViewModels
{
    public class ConditionVm
    {
        public int ConditionId { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(300)]
        [Required]
        public string Description { get; set; }

        [StringLength(100)]
        [Required]
        public string Symptoms { get; set; }

        public ICollection<BenefitVm> ConditionsHelpers { get; set; }
    }
}