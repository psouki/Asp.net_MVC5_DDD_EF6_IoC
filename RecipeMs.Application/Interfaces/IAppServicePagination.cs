using System.Collections.Generic;
using RecipeMs.CrossCutting.Common.Query;

namespace RecipeMs.Application.Interfaces
{
    public interface IAppServicePagination
    {
        string GetAllPaginated(int page, int numberPerPage);
        string FindPaginated(ICollection<QueryFilter> filters, int page, int numberPerPag);
    }
}
