using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class MyListService: ServiceBase<MyList>, IMyListService
    {
        public MyListService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
