using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RecipeMs.Web.Validations;

namespace RecipeMs.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(AntiScriptAttribute), 
                typeof(RegularExpressionAttributeAdapter));

            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}
