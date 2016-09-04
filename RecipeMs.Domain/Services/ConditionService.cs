using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class ConditionService: ServiceBase<Condition>, IConditionService
    {
        public ConditionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
