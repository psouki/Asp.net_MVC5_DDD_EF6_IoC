using Ninject.Modules;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Infra.Data.Repositories;

namespace RecipeMs.Infra.CrossCutting.IoC
{
    public class NinjectRepositoryModule: NinjectModule
    {
        public override void Load()
        {
            Bind(typeof (IRepositoryBase<>)).To(typeof (RepositoryBase<>));
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
        }
    }
}
