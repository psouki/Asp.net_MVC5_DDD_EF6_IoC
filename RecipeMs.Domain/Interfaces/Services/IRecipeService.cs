using System.Collections.Generic;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Domain.Interfaces.Services
{
    public interface IRecipeService : IServiceBase<Recipe>
    {
        IDictionary<Recipe, Recipe> GetRecipesToCompare(int recipeId1, int recipeId2);
    }
}
