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

namespace RecipeMs.Application
{
    public class BenefitAppService : AppServiceBase<Benefit>, IBenefitAppService
    {
        private readonly IBenefitService _service;
        public BenefitAppService(IBenefitService service) : base(service)
        {
            _service = service;
        }

        public string GetBenefitByIdWithFoods(int id)
        {
            ICollection<string> includes = new List<string>();
            includes.Add("Foods");

            Benefit benefit = _service.Get(b => b.BenefitId == id, includes);
            string result = benefit != null ? JsonHelper<Benefit>.Serialize(benefit) : string.Empty;

            return result;
        }


        //Update
        public new void Update(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            Benefit benefitData = JsonConvert.DeserializeObject<Benefit>(data);
            Benefit benefit = _service.GetById(benefitData.BenefitId);
            benefit.Title = benefitData.Title;
            benefit.Description = benefitData.Description;

            _service.Update(benefit);
            _service.Complete();
        }


        //Pagination
        public string GetAllPaginated(int page, int numberPerPage)
        {
            IEnumerable<Benefit> benefits = _service.GetAll().OrderBy(b => b.Title)
                                            .Select(b=> new Benefit
                                            {
                                                BenefitId = b.BenefitId,
                                                Title = b.Title,
                                                Description = b.Description.Length > 103 ? b.Description.Substring(0,100) + " ...": b.Description
                                            });
            IPagedList<Benefit> benefitsPaginated = benefits.ToPagedList(page, numberPerPage);
            string result = benefitsPaginated.TotalItemCount > 0 ? JsonHelper<Benefit>.SerializePagedList(benefitsPaginated) : string.Empty;
            return result;
        }
        public string FindPaginated(ICollection<QueryFilter> filters, int page, int numberPerPag)
        {
            Expression<Func<Benefit, bool>> query = ExpressionBuilder.GetExpression<Benefit>(filters);
            IEnumerable<Benefit> benefitList = _service.Find(query).OrderBy(b => b.Title)
                                                .Select(b => new Benefit
                                                {
                                                    BenefitId = b.BenefitId,
                                                    Title = b.Title,
                                                    Description = b.Description.Length > 103 ? b.Description.Substring(0, 100) + " ..." : b.Description
                                                });
            IPagedList<Benefit> benefits = benefitList.ToPagedList(page, numberPerPag);

            string result = benefits.TotalItemCount > 0 ? JsonHelper<Benefit>.SerializePagedList(benefits) : string.Empty;
            return result;
        }
        
    }
}
