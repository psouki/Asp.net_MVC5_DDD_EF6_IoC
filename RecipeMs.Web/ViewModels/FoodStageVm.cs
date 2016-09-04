using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RecipeMs.Web.ViewModels
{
    public class FoodStageVm
    {
        [HiddenInput(DisplayValue = false)]
        public int FoodStageId { get; set; }

        [StringLength(100)]
        [Required]
        public string Initial { get; set; }

        [StringLength(100)]
        [Required]
        public string Final { get; set; }

        [StringLength(100)]
        public string Complement { get; set; }

        [StringLength(100)]
        [Display(Name = "Refuse")]
        public string RefuseDescription { get; set; }

        [Display(Name = "%")]
        public decimal RefusePercentage { get; set; }

        [Display(Name = "Protein Factor")]
        public decimal ProteinCalorieFactor { get; set; }

        [Display(Name = "Fat Factor")]
        public decimal FatCalorieFactor { get; set; }

        [Display(Name = "Carb Factor")]
        public decimal CarbCalorieFactor { get; set; }

        public ICollection<NutritionalFactVm> NutritionalFacts { get; set; }

        [Required]
        public int FoodId { get; set; }
        public virtual FoodVm Food { get; set; }
    }
}