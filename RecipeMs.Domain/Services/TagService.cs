using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class TagService :ServiceBase<Tag>, ITagService
    {
        public TagService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
