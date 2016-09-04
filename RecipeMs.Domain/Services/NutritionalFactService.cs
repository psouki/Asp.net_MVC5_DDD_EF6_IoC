using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class NutritionalFactService: ServiceBase<NutritionalFact>, INutritionalFactService
    {
        public NutritionalFactService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
