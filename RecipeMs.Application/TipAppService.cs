using RecipeMs.Application.Interfaces;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Application
{
    public class TipAppService : AppServiceBase<Tip>, ITipAppService
    {
        public TipAppService(ITipService serviceBase) : base(serviceBase)
        {
        }
    }
}
