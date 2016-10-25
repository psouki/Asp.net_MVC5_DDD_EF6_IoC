using System.Web.Optimization;

namespace RecipeMs.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jsScripts")
                .Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery-ui-{version}.js",
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/AdminThemApp.js",
                    "~/Scripts/Own/Nav.js",
                    "~/Scripts/Own/Search.js",
                    "~/Scripts/Own/Crud.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));
           

            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                    "~/Content/bootstrap.css",
                    "~/Content/site.css",
                    "~/Content/Theme/Admin.css",
                    "~/Content/Theme/skin.css",
                    "~/Content/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/Nutrilabel")
                .Include("~/Scripts/Own/NutritionalLabel.js")
           );
        }
    }
}
