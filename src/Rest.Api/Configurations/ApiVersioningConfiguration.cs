using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Rest.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class ApiVersioningConfiguration
{
    public static void AddApiVersioningConfiguration(this IServiceCollection services)
    {
        services.AddApiVersioning(setup =>
        {
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
    }
}
