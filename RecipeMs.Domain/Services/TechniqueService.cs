using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class TechniqueService :ServiceBase<Technique>, ITechniqueService
    {
        public TechniqueService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
