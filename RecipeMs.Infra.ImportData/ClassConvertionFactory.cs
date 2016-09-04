using RecipeMs.Infra.ImportData.ConvertionType;
using RecipeMs.Infra.ImportData.Interfaces;

namespace RecipeMs.Infra.ImportData
{
    public class ClassConvertionFactory<TIn, TOut> where TIn: class where TOut : class 
    {
        public IClassConvertion<TIn, TOut> ClassConvertion = null;

        public IClassConvertion<TIn, TOut> Get(Enumerables.ClassConvertionEnum convertionType)
        {
            switch (convertionType)
            {
                case Enumerables.ClassConvertionEnum.UsdaFoodToFood:
                {
                    ClassConvertion = new UsdaFoodToFood<TIn,TOut>();
                    break;
                }
                case Enumerables.ClassConvertionEnum.UsdaNutriValuesToFoodStage:
                {
                    ClassConvertion = new UsdaNutriValuesToFoodStage<TIn,TOut>();
                    break;
                }
                case Enumerables.ClassConvertionEnum.UsdaFactDefinitionToFactDefinition:
                {
                    ClassConvertion = new UsdaFactDefinitionToFactDefinition<TIn, TOut>();
                    break;
                }
            }
            return ClassConvertion;
        }

    }
}
