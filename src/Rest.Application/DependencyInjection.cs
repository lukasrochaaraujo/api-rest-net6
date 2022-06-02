using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Rest.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(AppDomain.CurrentDomain.Load("Rest.Application"));
    }
}