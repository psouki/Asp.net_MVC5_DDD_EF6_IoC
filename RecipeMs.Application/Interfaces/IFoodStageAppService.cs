using System.Collections.Generic;

namespace RecipeMs.Application.Interfaces
{
    public interface IFoodStageAppService : IAppServiceBase, IAppServicePagination
    {
        IEnumerable<string> GetDistinctInitial();
        IEnumerable<string> GetDistinctFinal();
        string CreateNutritionalLabel(int foodStageId, decimal amout);
    }
}
