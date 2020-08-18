using Autofac;
using Microsoft.EntityFrameworkCore;

namespace Cognizant.Infrastructure.Data.PgSql.Challenges
{
    public class Module : Autofac.Module
    {
        public string ConnectionString { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseNpgsql(ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging(true);

            builder.RegisterType<Context>()
              .WithParameter(new TypedParameter(typeof(DbContextOptions), optionsBuilder.Options))
              .InstancePerLifetimeScope();

            builder.RegisterCognizantInfrastructureAssembly(GetType());
        }
    }
}
