using Autofac;
using BillApi.Repositories.Implemantations;
using BillApi.Repositories.Interfaces;

namespace BillApi.DependencyResolver.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<BillManager>().As<IBillService>();

        }
    }
}
