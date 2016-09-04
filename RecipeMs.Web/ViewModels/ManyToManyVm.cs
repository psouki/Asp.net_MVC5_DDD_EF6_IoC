using System.Collections.Generic;
using System.Web.Mvc;

namespace RecipeMs.Web.ViewModels
{
    public class ManyToManyVm
    {
        public int SelectId { get; set; }
        public int Id { get; set; }
        public IEnumerable<SelectListItem> SelectListItems { get; set; }
    }
}