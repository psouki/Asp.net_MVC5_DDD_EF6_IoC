using System.Collections.Generic;

namespace RecipeMs.Domain.Entities
{
    public class Tag
    {
        public Tag()
        {
            Recipes = new List<Recipe>();
        }

        public int TagId { get; set; }
        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
