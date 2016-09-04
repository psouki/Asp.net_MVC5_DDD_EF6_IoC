using Ninject.Modules;
using RecipeMs.Domain.Interfaces.Services;
using RecipeMs.Domain.Services;

namespace RecipeMs.Infra.CrossCutting.IoC
{
    public class NinjectServiceModule :NinjectModule
    {
        public override void Load()
        {
            Bind(typeof (IServiceBase<>)).To(typeof (ServiceBase<>));
            Bind<IBenefitService>().To<BenefitService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IConditionService>().To<ConditionService>();
            Bind<IFoodService>().To<FoodService>();
            Bind<IIngredientService>().To<IngredientService>();
            Bind<IMyListService>().To<MyListService>();
            Bind<INutritionalFactService>().To<NutritionalFactService>();
            Bind<IRecipeService>().To<RecipeService>();
            Bind<IStepService>().To<StepService>();
            Bind<ITagService>().To<TagService>();
            Bind<ITipService>().To<TipService>();
            Bind<IFactDefinitionService>().To<FactDefinitionService>();
            Bind<IFoodStageService>().To<FoodStageService>();
        }
    }
}
