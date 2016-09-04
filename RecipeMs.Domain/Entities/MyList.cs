using System.Collections.Generic;

namespace RecipeMs.Domain.Entities
{
    public class MyList
    {
        public int MyListId { get; set; }
        public string Name { get; set; }
        public string Query { get; set; }

        public ICollection<Recipe> GetLRecipesList()
        {
            ICollection<Recipe> recipes = new List<Recipe>();
            //Execute the query
            return recipes;
        }
    }
}
