using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Rest.Domain.TaskCardContext;
using Rest.Infrastructure.Mediator;
using Rest.Infrastructure.Data.Mappings;
using Rest.Infrastructure.Data.Repositories;
using Rest.Infrastructure.Identity;
using System;
using System.Text;

namespace Rest.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastrucureData(this IServiceCollection services)
        {
            TaskCardContextMap.Map();

            services.AddScoped<ITaskCardRepository, TaskCardRepository>();

            //todo: move configuration to appsettings approach
            services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb://172.23.157.243:27017"));
        }

        public static void AddInfrastrucureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var appSecretSection = configuration.GetSection(nameof(AppSecrets));
            services.Configure<AppSecrets>(appSecretSection);

            var appSecret = appSecretSection.Get<AppSecrets>();
            var key = Encoding.ASCII.GetBytes(appSecret.Secret);

            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(config =>
                    {
                        config.RequireHttpsMetadata = true;
                        config.SaveToken = true;
                        config.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidAudience = appSecret.Audience,
                            ValidIssuer = appSecret.Issuer
                        };
                    });
        }

        public static void AddInfrastrucureMediator(this IServiceCollection services)
        {
            var applicationAssembly = AppDomain.CurrentDomain.Load("Rest.Application");
            var InfrastructureAssembly = AppDomain.CurrentDomain.Load("Rest.Infrastructure");
            services.AddMediatR(applicationAssembly, InfrastructureAssembly);
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }
    }
}
