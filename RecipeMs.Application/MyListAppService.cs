using System;
using System.Collections.Generic;
using RecipeMs.Application.Interfaces;
using RecipeMs.CrossCutting.Common.Query;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Application
{
    public class MyListAppService: AppServiceBase<MyList>, IMyListAppService
    {
        public MyListAppService(IMyListService serviceBase): base(serviceBase)
        {
        }
    }
}
