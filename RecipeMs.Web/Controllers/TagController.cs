using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using RecipeMs.Application.Interfaces;
using RecipeMs.Application.Useful;
using RecipeMs.CrossCutting.Common.Query;
using RecipeMs.Web.Filters;
using RecipeMs.Web.ViewModels;
using static RecipeMs.Web.Util.ActionHelper;

namespace RecipeMs.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {
        private readonly ITagAppService _appService;

        public TagController(ITagAppService appService)
        {
            _appService = appService;
        }

        //search
        public ActionResult AutoComplete(string term)
        {
            term = term?.ToLower();
            ICollection<QueryFilter> filters = new List<QueryFilter>();
            QueryFilter filter = new QueryFilter("Name", term, Operator.StartsWith);
            filters.Add(filter);

            string result = _appService.Find(filters);
            IEnumerable<TagVm> tags = string.IsNullOrEmpty(result)
                ? new List<TagVm>().Select(t => new TagVm {Name = string.Empty})
                : JsonConvert.DeserializeObject<IEnumerable<TagVm>>(result);

            var model = tags.Select(l => new
            {
                label = l.Name
            });

            return JsonResult(model);
        }

        public ActionResult Index(string searchTerm)
        {
            searchTerm = searchTerm?.ToLower();

            string result;
            if (string.IsNullOrEmpty(searchTerm))
            {
                result = _appService.GetAll();
            }
            else
            {
                ICollection<QueryFilter> filters = new List<QueryFilter>();
                QueryFilter filter = new QueryFilter("Name", searchTerm, Operator.StartsWith);
                filters.Add(filter);

                result = _appService.Find(filters);
            }

            IEnumerable<TagVm> tags = string.IsNullOrEmpty(result)
                ? new List<TagVm>()
                : JsonConvert.DeserializeObject<IEnumerable<TagVm>>(result);

            tags = tags.OrderBy(t => t.Name);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_TagTable", tags);
            }

            return View(tags);
        }


        //creation
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagVm tag)
        {
            try
            {
                if (!ModelState.IsValid) return View(tag);

                string data = JsonHelper<TagVm>.Serialize(tag);
                _appService.Add(data);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(tag);
            }
        }


        //Edit
        [MissingParam(ParamName = "id")]
        public ActionResult Edit(int id)
        {
            string result = id == 0 ? string.Empty : _appService.GetById(id);
            TagVm tag = string.IsNullOrEmpty(result) ? null : JsonConvert.DeserializeObject<TagVm>(result);

            if (tag == null)
            {
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TagVm tag)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string data = JsonHelper<TagVm>.Serialize(tag);
                    _appService.Update(data);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(tag);
            }
        }


        //Delete
        [MissingParam(ParamName = "id")]
        public ActionResult Delete(int id)
        {
            string result = id == 0 ? string.Empty : _appService.GetById(id);
            TagVm tag = string.IsNullOrEmpty(result) ? null : JsonConvert.DeserializeObject<TagVm>(result);

            if (tag == null)
            {
                return RedirectToAction("Index");
            }

            return View(tag);
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
