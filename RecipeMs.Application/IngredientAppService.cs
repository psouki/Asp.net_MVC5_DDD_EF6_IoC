using RecipeMs.Application.Interfaces;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Application
{
    public class IngredientAppService: AppServiceBase<Ingredient>, IIngredientAppService
    {
        public IngredientAppService(IIngredientService serviceBase) :base(serviceBase)
        {
        }
    }
}
