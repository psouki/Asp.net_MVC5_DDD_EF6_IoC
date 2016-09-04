using System.Collections.Generic;
using System.Linq;

namespace RecipeMs.Domain.Entities
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public string Importance { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int MeasurementId { get; set; }
        public Measurement Measurement { get; set; }

        public int TechniqueId { get; set; }
        public Technique Techinique { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }


        public ICollection<NutritionalFact> CalculateNutritionalVNutritionalFacts()
        {
            //var nutritionalFacts = Food.NutritionalFacts as IList<NutritionalFact> ?? Food.NutritionalFacts.ToList();
            //foreach (NutritionalFact fact in nutritionalFacts)
            //{
            //    fact.Value = fact.Value*Quantity;
            //}

            return null;
        }
    }
}
