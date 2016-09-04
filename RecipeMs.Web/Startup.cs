using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipeMs.Web.Startup))]
namespace RecipeMs.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
