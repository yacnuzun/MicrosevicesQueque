using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Shared.Helpers.Security.JWT;
using Shared.Repositories.Implemantations;
using Shared.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();

            builder.RegisterType<SupplierHelper>().As<ISupplierHelper>().SingleInstance();

            builder.RegisterType<BuyerHelper>().As<IBuyerHelper>().SingleInstance();

            builder.RegisterType<FinancialHelper>().As<IFinancialHelper>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        }
    }
}
