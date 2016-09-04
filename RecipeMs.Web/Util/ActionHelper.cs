using System.Web.Mvc;
using RecipeMs.Web.Extensions;

namespace RecipeMs.Web.Util
{
    public class ActionHelper
    {
        public static ActionResult JsonResult(object data)
        {
            return new JsonNetResult() {Data = data};
        }
    }
}