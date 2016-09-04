using System.Collections.Generic;

namespace RecipeMs.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            Recipes = new List<Recipe>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
