using System;
using System.Web;
using Ninject;
using Ninject.Web.Common;
using RecipeMs.Infra.CrossCutting.IoC;

namespace RecipeMs.Infra.ImportData
{
    public class Ioc
    {
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel()
        {
            var kernel = RegisterServices(); //new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static StandardKernel RegisterServices()
        {
            StandardKernel ioc = new Container().GetServices();
            return ioc;
        }
    }
}
