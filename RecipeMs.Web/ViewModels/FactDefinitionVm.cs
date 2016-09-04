using System.ComponentModel.DataAnnotations;

namespace RecipeMs.Web.ViewModels
{
    public class FactDefinitionVm
    {
        public int FactDefinitioId { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Tag { get; set; }

        [StringLength(100)]
        [Required]
        public string Unit { get; set; }

        public decimal DecimalRoundNumber { get; set; }
        public int Parent { get; set; }
        public int Order { get; set; }
    }
}