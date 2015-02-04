using System;
using System.Linq;
using Ninject;

namespace OwinNinjectProject
{
    public static class NinjectConfig
    {
        public static IKernel RegisterNinject()
        {
            var kernel = new StandardKernel();

            //kernel.Load(Assembly.GetExecutingAssembly());

            //In order to load Ninject binding from all subprojects.
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => 
                assembly.FullName.StartsWith("Example", StringComparison.OrdinalIgnoreCase) ||
                assembly.FullName.StartsWith("Owin", StringComparison.OrdinalIgnoreCase));

            //kernel.Load(assemblies);
            foreach (var assembly in assemblies)
            {
                kernel.Load(assembly);
            }
            
            return kernel;
        }
    }
}