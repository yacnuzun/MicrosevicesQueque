using Autofac;
using FinancialAPI.Repositories.Implemantations;
using FinancialAPI.Repositories.Interfaces;

namespace FinancialAPI.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<FinancialHelper>().As<IFinancialHelper>();

        }
    }
}
