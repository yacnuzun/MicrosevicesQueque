
using Autofac.Extensions.DependencyInjection;
using Autofac;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.DependencyResolver.AutofacHelper;
using Shared.Events;
using Shared.Helpers;
using Shared.Helpers.Security.Encryption;
using Shared.Helpers.Security.JWT;
using Shared.Repositories.Implemantations;
using Shared.Repositories.Interfaces;
using Microsoft.OpenApi.Models;
using Shared.Constant;
using BuyerAPI.Consumer;

namespace BuyerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigurationManager configurationManager = builder.Configuration;

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            });

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
            
            var tokenOptions = configurationManager.GetSection("TokenOptions").Get<TokenOptions>();
            
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidIssuer = tokenOptions.Issuer,
                                    ValidAudience = tokenOptions.Audience,
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                                };
                            });
            builder.Services.AddSingleton<IBuyerHelper, BuyerHelper>();

            var rabbitOption = configurationManager.GetSection("RabbitOptions").Get<RabbitOptions>();

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<PaymentBillConsumer>();
                x.AddRequestClient<BillEvent>();
                x.UsingRabbitMq((context, _configurator) =>
                {
                    _configurator.Host(rabbitOption.Url, h =>
                    {
                        h.Username(rabbitOption.User);
                        h.Password(rabbitOption.Password);
                    });
                    _configurator.ReceiveEndpoint(RabbitMQSettings.Payment_OrderCreatedEventQueue, e =>
                    e.ConfigureConsumer<PaymentBillConsumer>(context));
                });

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
