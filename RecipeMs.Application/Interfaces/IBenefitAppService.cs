using RecipeMs.Domain.Entities;

namespace RecipeMs.Application.Interfaces
{
    public interface IBenefitAppService: IAppServiceBase, IAppServicePagination
    {
        string GetBenefitByIdWithFoods(int id);
    }
}
