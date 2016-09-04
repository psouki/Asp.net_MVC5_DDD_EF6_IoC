using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;

namespace RecipeMs.Domain.Services
{
    public class FoodStageService : ServiceBase<FoodStage>, IFoodStageService
    {
        public FoodStageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
