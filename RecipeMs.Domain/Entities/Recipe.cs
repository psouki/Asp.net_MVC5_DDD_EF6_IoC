using System.Collections.Generic;

namespace RecipeMs.Domain.Entities
{
    public class Recipe
    {
        public Recipe()
        {
            Tags = new List<Tag>();
            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();
        }

        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public int Course { get; set; }
        public decimal Rating { get; set; }
        public decimal Dificulty { get; set; }
        public bool Prepared { get; set; }
        public bool Star { get; set; }
        public int Prepareation { get; set; }
        public int Cooking { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Step> Steps { get; set; }
        public ICollection<Category> Categories { get; set; }

        public ICollection<NutritionalFact> CalculateNutrition()
        {
            IList<NutritionalFact> nutritionalFacts = new List<NutritionalFact>();
            //var ingredients = Ingredients as IList<Ingredient> ?? Ingredients.ToList();
            //foreach (NutritionalFact fact in ingredients.SelectMany(ingredient => ingredient.Food.NutritionalFacts))
            //{
            //    if (!nutritionalFacts.Contains(fact, new NutritionalFactComparer()))
            //    {
            //        nutritionalFacts.Add(fact);
            //    }
            //    else
            //    {
            //        nutritionalFacts.First(n => n.NutritionalFactId == fact.NutritionalFactId).Value += fact.Value;
            //    }
            //}

            return nutritionalFacts;
        }
        public ICollection<Recipe> SuggestRecipes()
        {
            IList<Recipe> recipes = new List<Recipe>();
            // Do the suggestion
            return recipes;
        }

    }
}
