using Autofac;
using SupplierAPI.Repositories.Implemantations;
using SupplierAPI.Repositories.Interfaces;

namespace SupplierAPI.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<SupplierHelper>().As<ISupplierHelper>();

        }
    }
}
