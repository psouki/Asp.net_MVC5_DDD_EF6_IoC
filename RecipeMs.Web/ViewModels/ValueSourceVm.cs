using System.ComponentModel.DataAnnotations;

namespace RecipeMs.Web.ViewModels
{
    public class ValueSourceVm
    {
        public int ValueSourceId { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [StringLength(100)]
        [Required]
        public string Authors { get; set; }

        [StringLength(100)]
        [Required]
        public int Year { get; set; }

        public int Journal { get; set; }
    }
}