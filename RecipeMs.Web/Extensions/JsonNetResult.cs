using System;
using System.Web;
using System.Web.Mvc;
using RecipeMs.Application.Useful;

namespace RecipeMs.Web.Extensions
{
    public class JsonNetResult : ActionResult
    {
        public object Data { get; set; }
        public string ContentType { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (Data != null)
            {
                string result = JsonHelper<object>.Serialize(Data);
                response.Write(result);
            }
        }
    }
}