using Newtonsoft.Json;
using RecipeMs.Application.Interfaces;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;
using RecipeMs.Domain.Services;

namespace RecipeMs.Application
{
    public class NutritionalFactAppService : AppServiceBase<NutritionalFact>, INutritionalFactAppService
    {
        private readonly INutritionalFactService _service;
        private readonly IFactDefinitionService _factDefinitionService;
        private readonly IFoodStageService _foodStageService;
        private readonly IValueSourceService _valueSourceService;

        public NutritionalFactAppService(INutritionalFactService service, IFactDefinitionService factDefinitionService, IFoodStageService foodStageService, IValueSourceService valueSourceService) : base(service)
        {
            _service = service;
            _factDefinitionService = factDefinitionService;
            _foodStageService = foodStageService;
            _valueSourceService = valueSourceService;
        }

        public void AddRelations(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            NutritionalFact nutritionalFactData = JsonConvert.DeserializeObject<NutritionalFact>(data);
            NutritionalFact nutritionalFact = _service.GetById(nutritionalFactData.NutritionalFactId);

            if (nutritionalFact.FoodStage != null)
            {
                FoodStage foodStage = _foodStageService.GetById(nutritionalFact.FoodStageId);
                nutritionalFact.FoodStage = foodStage;
            }
            else if (nutritionalFact.FactDefinition != null)
            {
                FactDefinition factDefinition = _factDefinitionService.GetById(nutritionalFact.FactDefinitioId);
                nutritionalFact.FactDefinition = factDefinition;

            }
            else if (nutritionalFact.ValueSource != null)
            {
                ValueSource valueSource = _valueSourceService.GetById(nutritionalFact.ValueSourceId);
                nutritionalFact.ValueSource = valueSource;
            }

            _service.Update(nutritionalFact);
            _service.Complete();
        }

        public void RemoveRelations(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

        }
    }
}
