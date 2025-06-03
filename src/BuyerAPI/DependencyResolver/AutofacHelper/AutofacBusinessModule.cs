using Autofac;
using BuyerAPI.Repositories.Implemantations;
using BuyerAPI.Repositories.Interfaces;

namespace BuyerAPI.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<BuyerHelper>().As<IBuyerHelper>();

        }
    }
}
