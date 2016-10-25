using System.Web;

namespace RecipeMs.Web.Modules
{
    public class XfoHeadersModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += Context_PreSendRequestHeaders;
        }

        private void Context_PreSendRequestHeaders(object sender, System.EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
            HttpContext.Current.Response.Headers.Remove("X-Frame-Options");
            HttpContext.Current.Response.Headers.Add("X-Frame-Options", "Deny");
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
        }

        public void Dispose()
        {
        }
    }
}