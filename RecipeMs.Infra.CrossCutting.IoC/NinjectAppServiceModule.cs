using Ninject.Modules;
using RecipeMs.Application;
using RecipeMs.Application.Interfaces;

namespace RecipeMs.Infra.CrossCutting.IoC
{
    public class NinjectAppServiceModule :  NinjectModule
    {
        public override void Load()
        {
            Bind(typeof (IAppServiceBase)).To(typeof (AppServiceBase<>));
            Bind<IBenefitAppService>().To<BenefitAppService>();
            Bind<ICategoryAppService>().To<CategoryAppService>();
            Bind<IConditionAppService>().To<ConditionAppService>();
            Bind<IFoodAppService>().To<FoodAppService>();
            Bind<IIngredientAppService>().To<IngredientAppService>();
            Bind<IMyListAppService>().To<MyListAppService>();
            Bind<INutritionalFactAppService>().To<NutritionalFactAppService>();
            Bind<IRecipeAppService>().To<RecipeAppService>();
            Bind<IStepAppService>().To<StepAppService>();
            Bind<ITagAppService>().To<TagAppService>();
            Bind<ITipAppService>().To<TipAppService>();
            Bind<IFoodStageAppService>().To<FoodStageAppService>();
        }
    }
}
