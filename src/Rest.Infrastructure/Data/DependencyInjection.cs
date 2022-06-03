using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Rest.Domain.TaskCardContext;
using Rest.Infrastrucutre.Data.Mappings;
using Rest.Infrastrucutre.Data.Repositories;

namespace Rest.Infrastrucutre.Data;

public static class DependencyInjection
{
    public static void AddInfrastrucureData(this IServiceCollection services)
    {
        TaskCardContextMap.Map();

        services.AddScoped<ITaskCardRepository, TaskCardRepository>();

        //todo: move configuration to appsettings approach
        services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb://172.24.58.128:27017"));
    }
}
