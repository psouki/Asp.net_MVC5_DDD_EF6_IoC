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
using RecipeMs.Domain.Interfaces.Services;
using RecipeMs.Domain.Services;

namespace RecipeMs.Application
{
    public class FoodAppService : AppServiceBase<Food>, IFoodAppService
    {
        private readonly IFoodService _service;
        private readonly IBenefitService _benefitService;
        private readonly IFoodStageService _foodStageService;

        public FoodAppService(IFoodService service, IBenefitService benefitService, IFoodStageService foodStageService) : base(service)
        {
            _service = service;
            _benefitService = benefitService;
            _foodStageService = foodStageService;
        }

        public string GetFoodByIdWithBenefits(int id)
        {
            ICollection<string> includes = new List<string>();
            includes.Add("Benefits");

            Food food = _service.Get(f => f.FoodId == id, includes);
            string result = JsonHelper<Food>.Serialize(food);
            return result;
        }

        //IT was not possible to build a generic update for classes with relationships. 
        //I use the new keyword because I didn't give up to do it generic yet, so this method was not intended to be overridden.
        public new void Update(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            Food foodData = JsonConvert.DeserializeObject<Food>(data);
            Food food = _service.GetById(foodData.FoodId);
            food.Name = foodData.Name;
            food.FoodGroup = foodData.FoodGroup;
            food.FoodSubgroup = foodData.FoodSubgroup;

            _service.Update(food);
            _service.Complete();
        }

        //Relations
        public void AddRelations(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            Food foodData = JsonConvert.DeserializeObject<Food>(data);
            Food food = _service.GetById(foodData.FoodId);

            if (foodData.Benefits.Any())
            {
                Benefit benefit = _benefitService.GetById(foodData.Benefits.First().BenefitId);
                food.Benefits.Add(benefit);
            }
            else if (foodData.FoodStages.Any())
            {
                FoodStage foodStage = _foodStageService.GetById(foodData.FoodStages.First().FoodStageId) ?? foodData.FoodStages.First();
                food.FoodStages.Add(foodStage);
            }

            _service.Update(food);
            _service.Complete();
        }

        public void RemoveRelations(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            Food foodData = JsonConvert.DeserializeObject<Food>(data);
            Food food = _service.GetById(foodData.FoodId);

            if (foodData.Benefits.Any())
            {
                Benefit benefit = _benefitService.GetById(foodData.Benefits.First().BenefitId);
                food.Benefits.Remove(benefit);
                _service.Update(food);
                _service.Complete();
            }
            else if (foodData.FoodStages.Any())
            {
                FoodStage foodStage = _foodStageService.GetById(foodData.FoodStages.First().FoodStageId);
                _foodStageService.Remove(foodStage);
                _foodStageService.Complete();
            }
        }

        //TODO find the best place to put the ordination
        //Pagination
        public string GetAllPaginated(int page, int numberPerPage)
        {
            IPagedList<Food> foodsPaginated = _service.GetAll().OrderBy(f=>f.Name).ToPagedList(page, numberPerPage);
            string result = foodsPaginated.TotalItemCount > 0 ? JsonHelper<Food>.SerializePagedList(foodsPaginated) : string.Empty;
            return result;
        }

        public string FindPaginated(ICollection<QueryFilter> filters, int page, int numberPerPag)
        {
            Expression<Func<Food, bool>> query = ExpressionBuilder.GetExpression<Food>(filters);
            IEnumerable<Food> foodsList = _service.Find(query).OrderBy(f => f.Name);
            IPagedList<Food> foods = foodsList.ToPagedList(page, numberPerPag);

            string result = foods.TotalItemCount > 0 ? JsonHelper<Food>.SerializePagedList(foods) : string.Empty;

            return result;
        }


    }
}
