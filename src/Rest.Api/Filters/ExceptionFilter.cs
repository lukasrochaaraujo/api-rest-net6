using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Rest.Api.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Rest.Api.Filters;

[ExcludeFromCodeCoverage]
public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        context.ExceptionHandled = true;
        CreateInternalServerError(context);
    }

    private void CreateInternalServerError(ExceptionContext context)
    {
        var problem = new ProblemDetail
        {
            Token = Guid.NewGuid().ToString(),
            StatusCode = StatusCodes.Status500InternalServerError,
            RequestPath = context.HttpContext.Request.Path,
            Message = "Unexpected error. To help with the solution, send the details with the generated token to the administrator."
        };

        _logger.LogError(context.Exception, $"[{problem.Token}] {problem.RequestPath} {context.Exception.Message}");

        context.Result = new JsonResult(problem);
    }
}