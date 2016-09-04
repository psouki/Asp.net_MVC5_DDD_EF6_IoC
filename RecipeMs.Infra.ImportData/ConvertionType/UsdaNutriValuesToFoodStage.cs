using System.Collections.Generic;
using System.Linq;
using Ninject;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;
using RecipeMs.Domain.Services;
using RecipeMs.Infra.ImportData.ImportEntities;
using RecipeMs.Infra.ImportData.Interfaces;

namespace RecipeMs.Infra.ImportData.ConvertionType
{
    public class UsdaNutriValuesToFoodStage<TIn, TOut> : IClassConvertion<TIn,TOut> where TIn :class where TOut: class 
    {
        public ICollection<TOut> ConverterEntities(ICollection<TIn> classesToConvert)
        {
            ICollection<UsdaNutriValue> nutriValues = (List<UsdaNutriValue>)classesToConvert;

            ICollection<NutritionalFact> nutritionalFacts = nutriValues.Select(nutriValue => new NutritionalFact
            {
                FactDefinition = new FactDefinition {UsdaNutriRefId = nutriValue.NutrNo},
                FoodStage = new FoodStage {UsdaRefId = nutriValue.NdbNo}, Value = nutriValue.NutrVal,
                Footnote = string.IsNullOrEmpty(nutriValue.Footnote) ? null : nutriValue.Footnote, ValueSourceId = 2
            }).ToList();

            ICollection<TOut> result = (List<TOut>) nutritionalFacts;
            return result;
        }

        public void Save(ICollection<TOut> classToSave, IKernel kernel)
        {
            INutritionalFactService service = kernel.Get<INutritionalFactService>();
            IFoodStageService foodStageService = kernel.Get<IFoodStageService>();
            IFactDefinitionService factDefinitionService = kernel.Get<IFactDefinitionService>();

            ICollection<NutritionalFact> nutritionalFacts = (List<NutritionalFact>) classToSave;
            foreach (NutritionalFact fact in nutritionalFacts)
            {
                FoodStage foodStage = foodStageService.Get(f => f.UsdaRefId == fact.FoodStage.UsdaRefId);
                if (foodStage == null) continue;
                FactDefinition factDefinition = factDefinitionService.Get(f => f.UsdaNutriRefId == fact.FactDefinition.UsdaNutriRefId);

                fact.FoodStage = foodStage;
                fact.FactDefinition = factDefinition;
                service.Add(fact);
            }
            service.Complete();
        }
    }
}
