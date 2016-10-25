using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PagedList;
using RecipeMs.Application.Interfaces;
using RecipeMs.Application.Useful;
using RecipeMs.CrossCutting.Common;
using RecipeMs.CrossCutting.Common.Query;
using RecipeMs.Web.Filters;
using RecipeMs.Web.ViewModels;

namespace RecipeMs.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        [MissingParam(ParamName = "id")]
        public ActionResult Details(int id)
        {
            string result = id == 0 ? string.Empty : _appService.GetById(id);
            BenefitVm benefit = string.IsNullOrEmpty(result)? null : JsonConvert.DeserializeObject<BenefitVm>(result);

            if (benefit == null)
            {
                return RedirectToAction("Index");
            }
            return View(benefit);
        }

        [HttpGet]
        [MissingParam(ParamName = "id")]
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

        [MissingParam(ParamName="id")]
        public ActionResult Edit(int id)
        {
            string result = id == 0 ? string.Empty : _appService.GetById(id);
            BenefitVm benefit = string.IsNullOrEmpty(result)
                ? new BenefitVm()
                : JsonConvert.DeserializeObject<BenefitVm>(result);

            if (benefit == null)
            {
                return RedirectToAction("Index");
            }

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

        [MissingParam(ParamName = "id")]
        public ActionResult Delete(int id)
        {
            string result = id == 0 ? string.Empty : _appService.GetById(id);
            BenefitVm benefit = string.IsNullOrEmpty(result)
                ? null
                : JsonConvert.DeserializeObject<BenefitVm>(result);

            if (benefit == null)
            {
                return RedirectToAction("Index");
            }
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
