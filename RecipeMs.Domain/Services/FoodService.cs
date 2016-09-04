using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class FoodService: ServiceBase<Food>, IFoodService
    {
        public FoodService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
