using Ninject;

namespace RecipeMs.Infra.CrossCutting.IoC
{
    public class Container
    {
        public StandardKernel GetServices()
        {
            return new StandardKernel(new NinjectRepositoryModule(),
                new NinjectServiceModule(),
                new NinjectAppServiceModule());
        }
    }
}
