using System;
using System.Web.Http;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(OwinNinjectProject.Startup))]

namespace OwinNinjectProject
{
    // Lazy Kernel based on: http://stackoverflow.com/questions/25351034/resolving-dependencies-in-owin-web-api-startup-cs-with-ninject
    // Ninject Dependency Resolver based on: http://stackoverflow.com/questions/23896806/how-to-use-ninject-bootstrapper-in-webapi-owinhost-startup

    public partial class Startup
    {
        private readonly Lazy<IKernel> _kernel = new Lazy<IKernel>(NinjectConfig.RegisterNinject);

        public void Configuration(IAppBuilder app)
        {
            ConfigureNinject(app);
            ConfigureAuth(app);
        }

        private void ConfigureNinject(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration;

            app.UseNinjectMiddleware(() => _kernel.Value);
            app.UseNinjectWebApi(config);

            System.Web.Http.Dependencies.IDependencyResolver ninjectResolver = new NinjectResolver(_kernel.Value);
            config.DependencyResolver = ninjectResolver;
        }
    }
}
