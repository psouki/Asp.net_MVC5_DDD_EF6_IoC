using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class RecipeService: ServiceBase<Recipe>, IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Recipe> _repository;

        public RecipeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<Recipe>();
        }

        public IDictionary<Recipe, Recipe> GetRecipesToCompare(int recipeId1, int recipeId2)
        {
            Recipe recipe1 = _repository.Find(r => r.RecipeId == recipeId1).FirstOrDefault();
            Recipe recipe2 = _repository.Find(r => r.RecipeId == recipeId2).FirstOrDefault();

            IDictionary<Recipe, Recipe> recipes = new Dictionary<Recipe, Recipe>();
            recipes.Add(recipe1, recipe2);

            return recipes;
        }
    }
}
