using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class StepService: ServiceBase<Step>, IStepService
    {
        public StepService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
