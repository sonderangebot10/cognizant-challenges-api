using Autofac;
using Cognizant.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cognizant.Infrastructure.Services.Rextester
{
    public class Module : Autofac.Module
    {
        public string Uri { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new Context(Uri))
                .InstancePerLifetimeScope();

            builder.RegisterType<ChallangesService>().As<IChallangesService>();
        }
    }
}
