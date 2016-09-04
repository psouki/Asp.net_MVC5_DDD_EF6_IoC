using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeMs.Web.ViewModels
{
    public class RecipeVm
    {
        public int RecipeId { get; set; }

        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        [Required]
        public string Source { get; set; }

        public int Course { get; set; }
        public decimal Rating { get; set; }
        public decimal Dificulty { get; set; }
        public bool Prepared { get; set; }
        public bool Star { get; set; }
        public int Preparation { get; set; }
        public int Cooking { get; set; }
    }
}