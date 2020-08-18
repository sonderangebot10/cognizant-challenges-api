using Autofac;

namespace Cognizant.ChallangesApi
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register all types of settings
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
