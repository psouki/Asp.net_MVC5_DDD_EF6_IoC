using System.ComponentModel.DataAnnotations;

namespace RecipeMs.Web.ViewModels
{
    public class NutritionalFactVm
    {
        public int NutritionalFactId { get; set; }

        [Required]
        public decimal Value { get; set; }
        public string Footnote { get; set; }

        [Required]
        public int FoodStageId { get; set; }
        public FoodStageVm FoodStage { get; set; }

        [Required]
        public int FactDefinitioId { get; set; }
        public FactDefinitionVm FactDefinition { get; set; }

        [Required]
        public int ValueSourceId { get; set; }
        public ValueSourceVm ValueSource { get; set; }

    }
}