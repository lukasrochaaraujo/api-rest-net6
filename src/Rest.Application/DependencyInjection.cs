using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rest.Application.Behaviours;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Rest.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = AppDomain.CurrentDomain.Load("Rest.Application");
        services.AddValidatorsFromAssembly(applicationAssembly);
        services.AddMediatR(applicationAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
    }
}