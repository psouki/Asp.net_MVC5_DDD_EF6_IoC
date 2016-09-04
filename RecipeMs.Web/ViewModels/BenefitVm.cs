using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RecipeMs.Web.ViewModels
{
    public class BenefitVm
    {
        public BenefitVm()
        {
            Foods = new List<FoodVm>();
        }

        [HiddenInput(DisplayValue = false)]
        public int BenefitId { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000)]
        [Required]
        public string Description { get; set; }

        public ICollection<ConditionVm> ConditionsHelped { get; set; }
        public ICollection<FoodVm> Foods { get; set; }
    }
}