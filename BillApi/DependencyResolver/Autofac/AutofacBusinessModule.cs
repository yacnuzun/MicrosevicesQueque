using Autofac;
using BillApi.Bussiness.Implemantations;
using BillApi.Bussiness.Interfaces;
using BillApi.Entities.DbConnectionContext;
using BillApi.Repositories.Implemantations;
using BillApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Persistance.Entities;
using Shared.Persistance.Implamantations;
using Shared.Persistance.Interfaces;

namespace BillApi.DependencyResolver.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<BillManager>().As<IBillService>();
            builder.RegisterType<EfBillRepository>().As<IBillRepository>();
            builder.RegisterType<EfUnitOfWork<BillDbContext>>().As<IUnitOfWork>();
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var opts = new DbContextOptionsBuilder<BillDbContext>()
                    .UseNpgsql(configuration["DbConnection:ConnectionString"])
                    .Options;
                return new BillDbContext(opts);
            })
.AsSelf()
.InstancePerLifetimeScope();
            builder.RegisterType<BillDbContext>().AsSelf().InstancePerLifetimeScope();
            

        }
    }
}
