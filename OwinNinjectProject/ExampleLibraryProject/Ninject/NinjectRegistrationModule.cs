using Ninject.Modules;
using Ninject.Web.Common;

namespace ExampleLibraryProject.Ninject
{
    public class NinjectRegistrationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFactory>().To<StringFactory>().InRequestScope();
        }
    }
}