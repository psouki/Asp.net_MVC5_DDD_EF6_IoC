using System.Collections.Generic;

namespace RecipeMs.Domain.Entities
{
    public class NutritionalLabel
    {
        public decimal Amount { get; set; }
        public decimal Calories { get; set; }
        public decimal CaloriesFromFat { get; set; }
        public decimal CaloriesFromProtein { get; set; }
        public decimal CaloriesFromCarb { get; set; }
        public decimal FatPercentage { get; set; }
        public decimal ProteinPercentage { get; set; }
        public decimal CarbPercentage { get; set; }
        public ICollection<Nutrient> Nutrients { get; set; }
    }
}
