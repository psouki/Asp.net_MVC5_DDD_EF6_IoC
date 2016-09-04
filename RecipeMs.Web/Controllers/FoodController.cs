using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using PagedList;
using RecipeMs.Application.Interfaces;
using RecipeMs.Application.Useful;
using RecipeMs.CrossCutting.Common;
using RecipeMs.Web.ViewModels;
using RecipeMs.CrossCutting.Common.Query;
using static RecipeMs.Web.Util.ActionHelper;

namespace RecipeMs.Web.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodAppService _foodAppService;
        private readonly IBenefitAppService _benefitAppService;
        private readonly IFoodStageAppService _foodStageAppService;

        public FoodController(IFoodAppService foodAppService, IBenefitAppService benefitAppService, IFoodStageAppService foodStageAppService)
        {
            _foodAppService = foodAppService;
            _benefitAppService = benefitAppService;
            _foodStageAppService = foodStageAppService;
        }

        //search
        public ActionResult AutoComplete(string term)
        {
            ICollection<QueryFilter> filters = new List<QueryFilter>();
            QueryFilter filter1 = new QueryFilter("Name", term, Operator.StartsWith);
            filters.Add(filter1);

            string result = _foodAppService.Find(filters);
            IEnumerable<FoodVm> foods = string.IsNullOrEmpty(result)
                ? new List<FoodVm>().Select(n => new FoodVm { Name = string.Empty })
                : JsonConvert.DeserializeObject<IEnumerable<FoodVm>>(result);
            var model = foods.Select(f => new
            {
                label = f.Name
            });

            return JsonResult(model);
        }

        public ActionResult Index(string searchTerm, int page = 1)
        {
            searchTerm = StringManipulation.CapitalizeName(searchTerm);
            string result;

            if (string.IsNullOrEmpty(searchTerm))
            {
                result = _foodAppService.GetAllPaginated(page, 20);
            }
            else
            {
                ICollection<QueryFilter> filters = new List<QueryFilter>();
                QueryFilter filter = new QueryFilter("Name", searchTerm, Operator.StartsWith);
                filters.Add(filter);

                result = _foodAppService.FindPaginated(filters, page, 20);
            }

            PaginationEntity<FoodVm> foodsPaginated = string.IsNullOrEmpty(result)
                ? new PaginationEntity<FoodVm>()
                : JsonConvert.DeserializeObject<PaginationEntity<FoodVm>>(result);

            SerializablePagedList<FoodVm> list = new SerializablePagedList<FoodVm>(foodsPaginated.Items, page, 20, foodsPaginated.MetaData.TotalItemCount);
            IPagedList<FoodVm> foodVms = list;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_foodTable", foodVms);
            }
            return View(foodVms);
        }


        //populate
        public ActionResult PopulateBenefitForFoods(int id)
        {
            ManyToManyVm manyToMany = new ManyToManyVm
            {
                Id = id,
                SelectListItems = GetBenefits()
            };

            return PartialView("_FoodBenefitDdl", manyToMany);
        }

        public ActionResult PopulateBenifitTable(int id)
        {
            string result = _foodAppService.GetFoodByIdWithBenefits(id);
            FoodVm food = result.Equals("null") ? new FoodVm() : JsonConvert.DeserializeObject<FoodVm>(result);
            IEnumerable<BenefitVm> benefits = food.Benefits?.Select(b => new BenefitVm()
            {
                BenefitId = b.BenefitId,
                Title = b.Title,
                Description = b.Description.Length > 49 ?  b.Description.Substring(0, 46) + " ..." : b.Description
            }).ToList() ?? new List<BenefitVm>();

            return PartialView("_BenefitTable", benefits);
        }

        public ActionResult PopulateFoodStageTable(int id, int page = 1)
        {
            ICollection<QueryFilter> filters = new List<QueryFilter>();
            QueryFilter filter = new QueryFilter("FoodId", id.ToString());
            filters.Add(filter);

            string result = _foodStageAppService.FindPaginated(filters, page, 7);

            PaginationEntity<FoodStageVm> foodStagePaginated = string.IsNullOrEmpty(result)
                ? new PaginationEntity<FoodStageVm>()
                : JsonConvert.DeserializeObject<PaginationEntity<FoodStageVm>>(result);

            SerializablePagedList<FoodStageVm> list = new SerializablePagedList<FoodStageVm>(foodStagePaginated.Items, page, 7, foodStagePaginated.MetaData.TotalItemCount);
            IPagedList<FoodStageVm> foodStages = list;

            return PartialView("_FoodStageTable", foodStages);
        }

        public ActionResult BuildNutritionalLabel(int id)
        {
            string result = _foodStageAppService.CreateNutritionalLabel(id, 100);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodVm food)
        {
            try
            {
                if (!ModelState.IsValid) return View(food);

                string data = JsonHelper<FoodVm>.Serialize(food);
                _foodAppService.Add(data);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(food);
            }
        }

        [HttpPost]
        public ActionResult AddBenefit(ManyToManyVm foodForm)
        {
            bool result = false;
            try
            {
                FoodVm food = new FoodVm { FoodId = foodForm.Id };
                food.Benefits.Add(new BenefitVm { BenefitId = foodForm.SelectId });

                string data = JsonHelper<FoodVm>.Serialize(food);
                _foodAppService.AddRelations(data);

                result = true;
            }
            catch (Exception ex)
            {

            }

            return Json(result);

        }

        public ActionResult AddFoodStage(int id)
        {
            IEnumerable<string> initialValues = _foodStageAppService.GetDistinctInitial();
            ViewBag.Initials = new SelectList(initialValues);

            IEnumerable<string> finalValues = _foodStageAppService.GetDistinctFinal();
            ViewBag.Finals = new SelectList(finalValues);

            FoodStageVm foodStage = new FoodStageVm { FoodId = id };
            return PartialView("_FoodStageMgm", foodStage);
        }

        [HttpPost]
        public ActionResult AddFoodStage(FoodStageVm foodStage)
        {
            bool result = false;

            try
            {
                FoodVm food = new FoodVm { FoodId = foodStage.FoodId };
                food.FoodStages.Add(foodStage);

                string data = JsonHelper<FoodVm>.Serialize(food);
                _foodAppService.AddRelations(data);

                result = true;

            }
            catch (Exception ex)
            {

            }
            return Json(result);
        }


        //Edit
        public ActionResult Edit(int id)
        {
            string result = _foodAppService.GetById(id);
            FoodVm food = string.IsNullOrEmpty(result) ? new FoodVm() : JsonConvert.DeserializeObject<FoodVm>(result);

            return View(food);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodVm food)
        {
            try
            {
                if (!ModelState.IsValid) return View(food);

                string data = JsonHelper<FoodVm>.Serialize(food);
                _foodAppService.Update(data);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(food);
            }
        }

        public ActionResult EditFoodStage(int id)
        {
            IEnumerable<string> initialValues = _foodStageAppService.GetDistinctInitial();
            ViewBag.Initials = new SelectList(initialValues);

            IEnumerable<string> finalValues = _foodStageAppService.GetDistinctFinal();
            ViewBag.Finals = new SelectList(finalValues);

            string result = _foodStageAppService.GetById(id);
            FoodStageVm foodStage = string.IsNullOrEmpty(result)
                ? new FoodStageVm {FoodId = id}
                : JsonConvert.DeserializeObject<FoodStageVm>(result);

            return PartialView("_FoodStageEdit", foodStage);
        }

        [HttpPost]
        public ActionResult EditFoodStage(FoodStageVm foodStage)
        {
            try
            {
                if (!ModelState.IsValid) PartialView("_FoodStageEdit", foodStage);

                string data = JsonHelper<FoodStageVm>.Serialize(foodStage);
                _foodStageAppService.Update(data);

                return Json(true);

            }
            catch (Exception ex)
            {

                return PartialView("_FoodStageEdit", foodStage);
            }
        }
        

        //Delete
        public ActionResult Delete(int id)
        {
            string result = _foodAppService.GetById(id);
            FoodVm food = string.IsNullOrEmpty(result) ? new FoodVm() : JsonConvert.DeserializeObject<FoodVm>(result);

            return View(food);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _foodAppService.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteBenefit(int parentId, int childId)
        {
            bool deleted = false;
            FoodVm food = new FoodVm { FoodId = parentId };
            food.Benefits.Add(new BenefitVm() { BenefitId = childId });

            try
            {
                string data = JsonHelper<FoodVm>.Serialize(food);
                _foodAppService.RemoveRelations(data);
                deleted = true;
            }
            catch (Exception ex)
            {

            }

            return Json(deleted);
        }

        [HttpPost]
        public ActionResult DeleteFoodStage(int parentId, int childId)
        {
            bool deleted = false;
            FoodVm food = new FoodVm { FoodId = parentId };
            food.FoodStages.Add(new FoodStageVm { FoodStageId = childId });

            try
            {
                string data = JsonHelper<FoodVm>.Serialize(food);
                _foodAppService.RemoveRelations(data);
                deleted = true;
            }
            catch (Exception ex)
            {

            }

            return Json(deleted);
        }


        //Private Methods
        private IEnumerable<SelectListItem> GetBenefits()
        {
            string result = _benefitAppService.GetAll();

            IEnumerable<BenefitVm> benefits = string.IsNullOrEmpty(result)
               ? new List<BenefitVm>()
               : JsonConvert.DeserializeObject<IEnumerable<BenefitVm>>(result);

            var benefitsList = benefits.Select(x => new SelectListItem
            {
                Value = x.BenefitId.ToString(),
                Text = x.Title
            }).OrderBy(b => b.Text).ToList();

            return new SelectList(benefitsList, "Value", "Text");
        }

       
    }
}
