using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class TipService: ServiceBase<Tip>, ITipService
    {
        public TipService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
