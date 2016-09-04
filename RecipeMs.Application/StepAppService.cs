using RecipeMs.Application.Interfaces;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Application
{
    public class StepAppService: AppServiceBase<Step>, IStepAppService
    {
        public StepAppService(IStepService serviceBase) : base(serviceBase)
        {
        }
    }
}
