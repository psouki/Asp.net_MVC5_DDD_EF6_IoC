using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class ValueSourceService: ServiceBase<ValueSource>, IValueSourceService
    {
        public ValueSourceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
