using RecipeMs.Domain.Entities;

namespace RecipeMs.Application.Interfaces
{
    public interface IFoodAppService: IAppServiceBase, IAppServiceRelations, IAppServicePagination
    {
        string GetFoodByIdWithBenefits(int id);
    }
}
