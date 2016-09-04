using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using RecipeMs.Web.Util;

namespace RecipeMs.Web.ViewModels
{
    public class FoodVm
    {
        public FoodVm()
        {
            Benefits = new List<BenefitVm>();
            FoodStages = new List<FoodStageVm>();
        }

        [HiddenInput(DisplayValue = false)]
        public int FoodId { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Group")]
        public Enumerators.FoodGroup FoodGroup { get; set; }

        [StringLength(100)]
        [Display(Name = "Sub Group")]
        public string FoodSubgroup { get; set; }

        public ICollection<FoodStageVm> FoodStages { get; set; }
        public ICollection<BenefitVm> Benefits { get; set; }
    }
}