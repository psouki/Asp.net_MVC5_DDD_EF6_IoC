using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class FactDefinitionService:ServiceBase<FactDefinition>, IFactDefinitionService
    {
        public FactDefinitionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
