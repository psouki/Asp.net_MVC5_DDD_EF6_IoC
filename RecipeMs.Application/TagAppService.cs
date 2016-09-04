using Newtonsoft.Json;
using RecipeMs.Application.Interfaces;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Application
{
    public class TagAppService : AppServiceBase<Tag>,  ITagAppService
    {
        private readonly ITagService _service;
        public TagAppService(ITagService service) : base(service)
        {
            this._service = service;
        }

        public new void Update(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            Tag tagdata = JsonConvert.DeserializeObject<Tag>(data);
            Tag tag = _service.GetById(tagdata.TagId);
            tag.Name = tagdata.Name;

            _service.Update(tag);
            _service.Complete();
        }
    }
}
