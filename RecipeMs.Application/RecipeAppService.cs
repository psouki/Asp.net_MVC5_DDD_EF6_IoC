using System;
using RecipeMs.Application.Interfaces;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Application
{
    public class RecipeAppService: AppServiceBase<Recipe>,  IRecipeAppService
    {
        private readonly IRecipeService _service;
        public RecipeAppService(IRecipeService serviceBase) : base(serviceBase)
        {
            _service = serviceBase;
        }

        public string GetRecipesToCompare(int recipeId1, int recipeId2)
        {
            throw new NotImplementedException();
        }
    }
}
