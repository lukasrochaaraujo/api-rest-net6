using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rest.Api.Configurations;
using Rest.Api.Filters;
using Rest.Application;
using Rest.Infrastrucutre.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => opt.Filters.Add<ExceptionFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioningConfiguration();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddHealthChecks();
builder.Services.AddApplication();
builder.Services.AddInfrastrucureData();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var apiVersionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerConfiguration(apiVersionProvider);
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
