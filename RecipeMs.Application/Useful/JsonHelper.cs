using System.Collections.Generic;
using Newtonsoft.Json;
using PagedList;

namespace RecipeMs.Application.Useful
{
    public class JsonHelper<T> where T: class 
    {
        public static string Serialize(T obj)
        {
           string result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            return result;
        }

        public static string Serialize(IEnumerable<T> obj)
        {
            string result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return result;
        }

        public static string SerializePagedList(IPagedList<T> pagedList)
        {
            string result = JsonConvert.SerializeObject(
                new
                {
                    Items = pagedList,
                    MetaData = pagedList.GetMetaData()
                },
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return result;
        }

    }
}
