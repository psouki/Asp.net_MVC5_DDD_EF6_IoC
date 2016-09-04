using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using PagedList;
using RecipeMs.Application.Interfaces;
using RecipeMs.Application.Useful;
using RecipeMs.CrossCutting.Common.Query;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Services;

namespace RecipeMs.Application
{
    public class FoodStageAppService : AppServiceBase<FoodStage>, IFoodStageAppService
    {
        private readonly IFoodStageService _service;

        public FoodStageAppService(IFoodStageService service) : base(service)
        {
            _service = service;
        }

        //IT was not possible to build a generic update for classes with relationships. 
        //I use the new keyword because I didn't give up to do it generic yet, so is not intended to override the method.
        public new void Update(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            FoodStage foodStageData = JsonConvert.DeserializeObject<FoodStage>(data);
            FoodStage foodStage = _service.GetById(foodStageData.FoodStageId);
            foodStage.Initial = foodStageData.Initial;
            foodStage.Final = foodStageData.Final;
            foodStage.Complement = foodStageData.Complement;
            foodStage.RefuseDescription = foodStageData.RefuseDescription;
            foodStage.RefusePercentage = foodStageData.RefusePercentage;
            foodStage.ProteinCalorieFactor = foodStageData.ProteinCalorieFactor;
            foodStage.FatCalorieFactor = foodStageData.FatCalorieFactor;
            foodStage.CarbCalorieFactor = foodStageData.CarbCalorieFactor;

            _service.Update(foodStage);
            _service.Complete();
        }

        public IEnumerable<string> GetDistinctInitial()
        {
            var stages = _service.GetAll().GroupBy(s => s.Initial).Select(s => s).ToList();
            ICollection<string> result = stages.OrderBy(s=>s.Key).Select(stage => stage.Key).ToList();
            return result;
        }

        public IEnumerable<string> GetDistinctFinal()
        {
            var stages = _service.GetAll().GroupBy(s => s.Final).Select(s => s).ToList();
            ICollection<string> result = stages.OrderBy(s => s.Key).Select(stage => stage.Key).ToList();
            return result;
        }

        public string CreateNutritionalLabel(int foodStageId, decimal amount)
        {
            ICollection<string> includes = new List<string>();
            includes.Add("NutritionalFacts");
            includes.Add("NutritionalFacts.FactDefinition");

            FoodStage foodStage = _service.Get(f=> f.FoodStageId == foodStageId, includes);
            NutritionalLabel nutritionalLabel = foodStage?.CreateNutritionalLabel(amount);

            string result = nutritionalLabel != null ? JsonHelper<NutritionalLabel>.Serialize(nutritionalLabel) : string.Empty;

            return result;
        }

        public string GetAllPaginated(int page, int numberPerPage)
        {
            IPagedList<FoodStage> foodsSatgePaginated = _service.GetAll().ToPagedList(page, numberPerPage);
            string result = foodsSatgePaginated.TotalItemCount > 0 ? JsonHelper<FoodStage>.SerializePagedList(foodsSatgePaginated) : string.Empty;
            return result;
        }

        public string FindPaginated(ICollection<QueryFilter> filters, int page, int numberPerPag)
        {
            Expression<Func<FoodStage, bool>> query = ExpressionBuilder.GetExpression<FoodStage>(filters);
            IEnumerable<FoodStage> foodsSTageList = _service.Find(query).OrderBy(f => f.Initial).ThenBy(f=>f.Final);
            IPagedList<FoodStage> foodStages = foodsSTageList.ToPagedList(page, numberPerPag);

            string result = foodStages.TotalItemCount > 0 ? JsonHelper<FoodStage>.SerializePagedList(foodStages) : string.Empty;

            return result;
        }
    }
}
