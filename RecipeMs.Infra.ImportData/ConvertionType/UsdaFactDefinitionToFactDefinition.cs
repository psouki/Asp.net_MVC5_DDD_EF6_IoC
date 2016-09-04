using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;
using RecipeMs.Infra.ImportData.ImportEntities;
using RecipeMs.Infra.ImportData.Interfaces;

namespace RecipeMs.Infra.ImportData.ConvertionType
{
    public class UsdaFactDefinitionToFactDefinition<TIn, TOut> : IClassConvertion<TIn, TOut> where TIn :class where TOut : class 
    {
       public ICollection<TOut> ConverterEntities(ICollection<TIn> classesToConvert)
        {
            ICollection<UsdaFactDefinition> factDefinitions = (List<UsdaFactDefinition>) classesToConvert;

            return (ICollection<TOut>) factDefinitions.Select(usdaFactDefinition => new FactDefinition
            {
                Name = usdaFactDefinition.NutrDesc,
                Unit = usdaFactDefinition.Unit,
                Tag = usdaFactDefinition.Tagname,
                DecimalRoundNumber = usdaFactDefinition.NumDec,
                Order = usdaFactDefinition.Order,
                UsdaNutriRefId = usdaFactDefinition.NutrNo
            }).ToList();
        }

        public void Save(ICollection<TOut> classToSave, IKernel kernel)
        {
            IFactDefinitionService service = kernel.Get<IFactDefinitionService>();
            ICollection<FactDefinition> factDefinitions = (List<FactDefinition>) classToSave;
            foreach (FactDefinition factDefinition in factDefinitions)
            {
                service.Add(factDefinition);
            }
            service.Complete();
        }
    }
}
