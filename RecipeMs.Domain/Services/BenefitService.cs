using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class BenefitService: ServiceBase<Benefit>, IBenefitService
    {
        public BenefitService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
