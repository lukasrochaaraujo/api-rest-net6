﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Rest.Infrastrucutre.Identity.Authetication.Jwt
{
    public static class DependencyInjection
    {
        public static void AddAutenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
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
    }
}
