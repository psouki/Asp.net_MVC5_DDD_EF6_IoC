using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using RecipeMs.Web.Validations;

namespace RecipeMs.Web.ViewModels
{
    public class TagVm
    {
        [HiddenInput(DisplayValue = false)]
        public int TagId { get; set; }

        [StringLength(100)]
        [Required]
        [AntiScript]
        public string Name { get; set; }

        public ICollection<RecipeVm> Recipes { get; set; }
    }
}