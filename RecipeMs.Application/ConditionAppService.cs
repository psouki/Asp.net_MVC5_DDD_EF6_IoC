using System.Collections.Generic;
using RecipeMs.Application.Interfaces;
using RecipeMs.CrossCutting.Common.Query;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Application
{
    public class ConditionAppService : AppServiceBase<Condition>, IConditionAppService
    {
        public ConditionAppService(IConditionService serviceBase) : base(serviceBase)
        {
        }
    }
}
