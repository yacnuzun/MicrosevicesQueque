using AccountApi.Application.Services.Implementations;
using AccountApi.Application.Services.Interfaces;
using AccountApi.Application.Validators;
using AccountApi.Dto_s;
using AccountApi.Infrastructure.Data;
using AccountApi.Infrastructure.Helpers.JWT;
using AccountApi.Infrastructure.Repositories.Implemantations;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Autofac;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Persistance.Implamantations;
using Shared.Persistance.Interfaces;

namespace AccountApi.Infrastructure.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<UserOperationClaimRepository>().As<IUserOperationClaimRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<OperationClaimRepository>().As<IOperationClaimRepository>();
            builder.RegisterType<EfUnitOfWork<AccountDbContext>>().As<IUnitOfWork>();
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var opts = new DbContextOptionsBuilder<AccountDbContext>()
                    .UseNpgsql(configuration["DbConnection:ConnectionString"])
                    .Options;
                return new AccountDbContext(opts);
            })
            .AsSelf()
            .InstancePerLifetimeScope();
            builder.RegisterType<ClaimValidator>().As<IValidator<ClaimDto>>().InstancePerLifetimeScope();
        }
    }
}
