using AccountApi.Helpers.JWT;
using AccountApi.Repositories.Implemantations;
using AccountApi.Repositories.Interfaces;
using Autofac;

namespace AccountApi.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<UserManager>().As<IUserService>();

        }
    }
}
