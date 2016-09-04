using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RecipeMs.Application.Interfaces;
using RecipeMs.Application.Useful;
using RecipeMs.Web.ViewModels;
using PagedList;
using RecipeMs.CrossCutting.Common;
using RecipeMs.CrossCutting.Common.Query;

namespace RecipeMs.Web.Controllers
{
    public class BenefitController : Controller
    {
        private readonly IBenefitAppService _appService;

        public BenefitController(IBenefitAppService appService)
        {
            _appService = appService;
        }

        public ActionResult Index(string searchTerm, int page = 1)
        {
            searchTerm = StringManipulation.CapitalizeName(searchTerm);
            string result;

            if (string.IsNullOrEmpty(searchTerm))
            {
                result = _appService.GetAllPaginated(page, 5);
            }
            else
            {
                ICollection<QueryFilter> filters = new List<QueryFilter>();
                QueryFilter filter = new QueryFilter("Title", searchTerm, Operator.StartsWith);
                filters.Add(filter);

                result = _appService.FindPaginated(filters, page, 5);
            }

            PaginationEntity<BenefitVm> benefitsPaginated = string.IsNullOrEmpty(result)
                ? new PaginationEntity<BenefitVm>()
                : JsonConvert.DeserializeObject<PaginationEntity<BenefitVm>>(result);

            SerializablePagedList<BenefitVm> list = new SerializablePagedList<BenefitVm>(benefitsPaginated.Items, page, 5, benefitsPaginated.MetaData.TotalItemCount);
            IPagedList<BenefitVm> benefitVms = list;


            if (Request.IsAjaxRequest())
            {
                return PartialView("_BenefitTable", benefitVms);
            }

            return View(benefitVms);
        }

        public ActionResult Details(int id=1)
        {
            string result = _appService.GetById(id);
            BenefitVm benefit = string.IsNullOrEmpty(result)? new BenefitVm() : JsonConvert.DeserializeObject<BenefitVm>(result);

            return View(benefit);
        }

        public ActionResult PopulateFoods(int id, int page = 1)
        {
            string result = _appService.GetBenefitByIdWithFoods(id);
            BenefitVm benefit = string.IsNullOrEmpty(result)
                ? new BenefitVm()
                : JsonConvert.DeserializeObject<BenefitVm>(result);

            IPagedList<FoodVm> foodList = benefit.Foods.ToPagedList(page, 20);

            return PartialView("_FoodTable", foodList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BenefitVm benefit)
        {
            try
            {
                if (!ModelState.IsValid) return View(benefit);

                string data = JsonHelper<BenefitVm>.Serialize(benefit);
                _appService.Add(data);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(benefit);
            }
        }

        public ActionResult Edit(int id)
        {
            string result = _appService.GetById(id);
            BenefitVm benefit = string.IsNullOrEmpty(result)
                ? new BenefitVm()
                : JsonConvert.DeserializeObject<BenefitVm>(result);

            return View(benefit);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BenefitVm benefit)
        {
            try
            {
                if (!ModelState.IsValid) return View(benefit);

                string data = JsonHelper<BenefitVm>.Serialize(benefit);
                _appService.Update(data);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(benefit);
            }
        }

        public ActionResult Delete(int id)
        {
            string result = _appService.GetById(id);
            BenefitVm benefit = string.IsNullOrEmpty(result)
                ? new BenefitVm()
                : JsonConvert.DeserializeObject<BenefitVm>(result);

            return View(benefit);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _appService.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
