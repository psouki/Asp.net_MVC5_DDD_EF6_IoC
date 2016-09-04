using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class IngredientService : ServiceBase<Ingredient>, IIngredientService
    {
        public IngredientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
