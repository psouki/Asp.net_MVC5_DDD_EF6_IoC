using System.Web.Mvc;

namespace RecipeMs.Web.Filters
{
    public class MissingParamAttribute : ActionFilterAttribute
    {
        public string ParamName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey(ParamName))
            {
                var parameter = filterContext.ActionParameters[ParamName];
                if (parameter == null )
                {
                    filterContext.ActionParameters[ParamName] = 0;
                }
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}