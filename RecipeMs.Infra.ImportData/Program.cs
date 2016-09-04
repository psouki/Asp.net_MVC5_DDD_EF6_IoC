using System;
using System.Collections.Generic;
using Ninject;
using RecipeMs.Domain.Entities;
using RecipeMs.Infra.ImportData.ImportEntities;

namespace RecipeMs.Infra.ImportData
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = Ioc.CreateKernel();

            // Food and Food Stages Population
            //PopulateFood(kernel);

            //Fact Definition population
            //PopulateDefinition(kernel);

            //Nutritional values population
            PopulateNutritionalValues(kernel);


            Console.ReadKey();
        }

        public static void PopulateFood(IKernel kernel)
        {
            //ICollection<UsdaFood> usdaFoodsfoods = Deserializer.GetDesirialized<UsdaFood>(AppDomain.CurrentDomain.BaseDirectory + "\\newFoods.xml");
           // ICollection<UsdaFood> usdaFoodsfoods = Deserializer.GetDesirialized<UsdaFood>(AppDomain.CurrentDomain.BaseDirectory + "\\newFish.xml");
            ICollection<UsdaFood> usdaFoodsfoods = Deserializer.GetDesirialized<UsdaFood>(AppDomain.CurrentDomain.BaseDirectory + "\\restoPg40.xml");
            ClassConvertionFactory<UsdaFood, Food> factory = new ClassConvertionFactory<UsdaFood, Food>();
            ICollection<Food> foods = factory.Get(Enumerables.ClassConvertionEnum.UsdaFoodToFood).ConverterEntities(usdaFoodsfoods);
            factory.ClassConvertion.Save(foods, kernel);
        }
        public static void PopulateDefinition(IKernel kernel)
        {
            ICollection<UsdaFactDefinition> usdaFactDefinitions = Deserializer.GetDesirialized<UsdaFactDefinition>(AppDomain.CurrentDomain.BaseDirectory + "\\NUTR_DEF.xml");
            ClassConvertionFactory<UsdaFactDefinition, FactDefinition> factory = new ClassConvertionFactory<UsdaFactDefinition, FactDefinition>();
            ICollection<FactDefinition> factDefinitions = factory.Get(Enumerables.ClassConvertionEnum.UsdaFactDefinitionToFactDefinition).ConverterEntities(usdaFactDefinitions);
            factory.ClassConvertion.Save(factDefinitions, kernel);
        }
        private static void PopulateNutritionalValues(IKernel kernel)
        {
            ICollection<UsdaNutriValue> usdaFactDefinitions = Deserializer.GetDesirialized<UsdaNutriValue>(AppDomain.CurrentDomain.BaseDirectory + "\\nutriValues.xml");
            ClassConvertionFactory<UsdaNutriValue, NutritionalFact> factory = new ClassConvertionFactory<UsdaNutriValue, NutritionalFact>();
            ICollection<NutritionalFact> nutritionalFacts = factory.Get(Enumerables.ClassConvertionEnum.UsdaNutriValuesToFoodStage).ConverterEntities(usdaFactDefinitions);
            factory.ClassConvertion.Save(nutritionalFacts, kernel);

        }
    }
}
