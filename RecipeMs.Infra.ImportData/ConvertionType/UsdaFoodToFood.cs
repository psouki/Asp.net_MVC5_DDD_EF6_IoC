using System.Collections.Generic;
using System.Linq;
using Ninject;
using RecipeMs.CrossCutting.Common;
using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Services;
using RecipeMs.Infra.ImportData.ImportEntities;
using RecipeMs.Infra.ImportData.Interfaces;

namespace RecipeMs.Infra.ImportData.ConvertionType
{
    public class UsdaFoodToFood<TIn, TOut> : IClassConvertion<TIn, TOut> where TIn : class where TOut : class
    {
        public ICollection<TOut> ConverterEntities(ICollection<TIn> classesToConvert)
        {
            ICollection<Food> foods = new List<Food>();
            ICollection<UsdaFood> usdaFoods = (ICollection<UsdaFood>)classesToConvert;
            IEnumerable<IGrouping<string, UsdaFood>> groupedFoods = usdaFoods.GroupBy(g => g.Name);
            foreach (var foodGroup in groupedFoods)
            {
                Food food = new Food
                {
                    Name = StringManipulation.CapitalizeName(foodGroup.First().Name),
                    FoodGroup = CorrectGroupId(foodGroup.First().GroupId),
                    FoodSubgroup = foodGroup.First().Subgroup,
                    FoodStages = new List<FoodStage>()
                };
                foreach (FoodStage foodStage in foodGroup.Select(stage => new FoodStage
                {
                    Food = food,
                    Final = stage.FinalStage?.ToLower().Trim(),
                    Initial = stage.InitialStage?.ToLower().Trim(),
                    Complement = stage.Complement?.ToLower().Trim(),
                    RefuseDescription = stage.RefuseDesc,
                    RefusePercentage = stage.Refuse,
                    ProteinCalorieFactor = stage.ProteinFactor,
                    FatCalorieFactor = stage.FatFactor,
                    CarbCalorieFactor = stage.CarbFactor,
                    UsdaRefId = stage.UsdaRefId
                }))
                {
                    food.FoodStages.Add(foodStage);
                }
                foods.Add(food);
            }

            ICollection<TOut> result = (List<TOut>)foods;

            return result;
        }

        public void Save(ICollection<TOut> classToSave, IKernel kernel)
        {
            IFoodService service = kernel.Get<IFoodService>();
            ICollection<Food> foods = (List<Food>)classToSave;
            foreach (Food food in foods)
            {
                service.Add(food);
            }
            service.Complete();
        }

        private static int CorrectGroupId(int oldGroup)
        {
            int groupId = 0;

            switch (oldGroup)
            {
                case 100:
                    groupId = 1;
                    break;
                case 200:
                    groupId = 2;
                    break;
                case 400:
                    groupId = 3;
                    break;
                case 500:
                    groupId = 4;
                    break;
                case 600:
                    groupId = 5;
                    break;
                case 700:
                    groupId = 6;
                    break;
                case 900:
                    groupId = 7;
                    break;
                case 1000:
                    groupId = 8;
                    break;
                case 1100:
                    groupId = 9;
                    break;
                case 1200:
                    groupId = 10;
                    break;
                case 1300:
                    groupId = 11;
                    break;
                case 1400:
                    groupId = 12;
                    break;
                case 1500:
                    groupId = 13;
                    break;
                case 1600:
                    groupId = 14;
                    break;
                case 1700:
                    groupId = 15;
                    break;
                case 1800:
                    groupId = 16;
                    break;
                case 1900:
                    groupId = 17;
                    break;
                case 2000:
                    groupId = 18;
                    break;
            }

            return groupId;
        }

        private static string FormatInitiaFoodStage(string initialStage, int group)
        {
            initialStage = initialStage?.ToLower().Trim();
            if (!string.IsNullOrEmpty(initialStage)) return initialStage;
            switch (group)
            {
                case 2000:
                case 1600:
                case 700:
                case 1200:
                    initialStage = "crude";
                    break;
                case 1900:
                case 1800:
                case 1400:
                case 400:
                case 100:
                case 800:
                    initialStage = "as purchased";
                    break;
                case 1700:
                case 1500:
                case 1300:
                case 1000:
                case 1100:
                case 500:
                case 600:
                case 900:
                    initialStage = "fresh";
                    break;
            }

            return initialStage;
        }

        private static string FormatFinalFoodStage(string finalStage, int group)
        {
            finalStage = finalStage?.ToLower().Trim();
            if (string.IsNullOrEmpty(finalStage) || finalStage == "raw")
            {
                finalStage = "uncooked";
            }
            return finalStage;
        }
    }
}
