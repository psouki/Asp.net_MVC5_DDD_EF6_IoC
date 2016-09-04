using RecipeMs.Application.Interfaces;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Application
{
    public class CategoryAppService: AppServiceBase<Category>, ICategoryAppService
    {
        public CategoryAppService(ICategoryService serviceBase) : base(serviceBase)
        {
        }
    }
}
