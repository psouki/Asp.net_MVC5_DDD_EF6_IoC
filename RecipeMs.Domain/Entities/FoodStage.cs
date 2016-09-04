using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeMs.Domain.Entities
{
    public class FoodStage
    {
        public FoodStage()
        {
            NutritionalFacts = new List<NutritionalFact>();
        }

        public int FoodStageId { get; set; }
        public string Initial { get; set; }
        public string Final { get; set; }
        public string Complement { get; set; }
        public string RefuseDescription { get; set; }
        public decimal RefusePercentage { get; set; }
        public decimal ProteinCalorieFactor { get; set; }
        public decimal FatCalorieFactor { get; set; }
        public decimal CarbCalorieFactor { get; set; }
        public int UsdaRefId { get; set; }

        public ICollection<NutritionalFact> NutritionalFacts { get; set; }

        public int FoodId { get; set; }
        public virtual Food Food { get; set; }

        //TODO; Verify the math in case of food stage with refuse
        public NutritionalLabel CreateNutritionalLabel(decimal amount)
        {
            NutritionalLabel label = new NutritionalLabel { Amount = amount };

            NutritionalFact calories = NutritionalFacts.FirstOrDefault(n => n.FactDefinition.Tag != null && n.FactDefinition.Tag.Equals("ENERC_KCAL"));
            label.Calories = calories?.Value ?? 0;

            NutritionalFact fat = NutritionalFacts.FirstOrDefault(n => n.FactDefinition.Tag.Equals("FAT"));
            decimal fatValue = fat?.Value ?? 0;
            label.CaloriesFromFat = FatCalorieFactor > 0 ? fatValue * FatCalorieFactor : fatValue * 9;

            NutritionalFact protein = NutritionalFacts.FirstOrDefault(n => n.FactDefinition.Tag != null &&  n.FactDefinition.Tag.Equals("PROCNT"));
            decimal proteinValue = protein?.Value ?? 0;
            label.CaloriesFromProtein = ProteinCalorieFactor > 0 ? ProteinCalorieFactor * proteinValue : proteinValue * 4;

            NutritionalFact carb = NutritionalFacts.FirstOrDefault(n => n.FactDefinition.Tag != null && n.FactDefinition.Tag.Equals("CHOCDF"));
            decimal carbValue = carb?.Value ?? 0;
            label.CaloriesFromCarb = CarbCalorieFactor > 0 ? carbValue * CarbCalorieFactor : carbValue * 4;

            label = FixPercentages(label);
            label = PopulateNutrient(label, false);

            return label;
        }

        private NutritionalLabel PopulateNutrient(NutritionalLabel label, bool complexity)
        {
            label.Nutrients = new List<Nutrient>();
            IEnumerable<NutritionalFact> nutrientValues = complexity 
                ? NutritionalFacts.Where(n => n.FactDefinition.Tag != "ENERC_KCAL") 
                : NutritionalFacts.Where(n => n.FactDefinition.Tag != "ENERC_KCAL" && n.FactDefinition.IsPrincipal);

            nutrientValues = nutrientValues.OrderBy(n => n.FactDefinition.Order);

            foreach (Nutrient nutrient in from nutrientValue in nutrientValues where Math.Round(nutrientValue.Value, nutrientValue.FactDefinition.DecimalRoundNumber) > 0
                    select new Nutrient
                    {
                        Value = Math.Round(nutrientValue.Value, nutrientValue.FactDefinition.DecimalRoundNumber),
                        IsTitle = nutrientValue.FactDefinition.IsTitle,
                        Name = nutrientValue.FactDefinition.Name,
                        Order = nutrientValue.FactDefinition.Order,
                        Type = nutrientValue.FactDefinition.Type,
                        Unit = nutrientValue.FactDefinition.Unit,
                        ShowPercentage = nutrientValue.FactDefinition.DailyValue > 0,
                        Percentage =
                            nutrientValue.FactDefinition.DailyValue > 0
                            ? Math.Round((nutrientValue.Value / nutrientValue.FactDefinition.DailyValue)*100, 2)
                            : 0
                        })
            {
                label.Nutrients.Add(nutrient);
            }
            return label;
        }
        private static NutritionalLabel FixPercentages(NutritionalLabel label)
        {
            label.FatPercentage = label.CaloriesFromFat > 0 ? Math.Round(((label.CaloriesFromFat / label.Calories) * 100), 0) : 0;
            label.ProteinPercentage = label.CaloriesFromProtein > 0 ? Math.Round(((label.CaloriesFromProtein / label.Calories) * 100), 0) : 0;
            label.CarbPercentage = label.CaloriesFromCarb > 0 ? Math.Round(((label.CaloriesFromCarb / label.Calories) * 100), 0) : 0;

            decimal residualPercentage = 100 - (label.FatPercentage + label.ProteinPercentage + label.CarbPercentage);

            if (residualPercentage == 0 || residualPercentage == 100) return label;

            if (label.FatPercentage >= label.ProteinPercentage && label.FatPercentage >= label.CarbPercentage)
            {
                label.FatPercentage += residualPercentage;
            }
            else if (label.CarbPercentage >= label.FatPercentage && label.CarbPercentage >= label.ProteinPercentage)
            {
                label.CarbPercentage += residualPercentage;
            }
            else if (label.ProteinPercentage > label.FatPercentage && label.ProteinPercentage > label.CarbPercentage)
            {
                label.ProteinPercentage += residualPercentage;
            }

            return label;
        }
    }
}
