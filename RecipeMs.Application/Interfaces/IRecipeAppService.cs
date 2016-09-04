using System.Collections.Generic;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Application.Interfaces
{
    public interface IRecipeAppService : IAppServiceBase
    {
        string GetRecipesToCompare(int recipeId1, int recipeId2);
    }
}
