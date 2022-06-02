using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
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
        var exceptionModel = new
        {
            Token = Guid.NewGuid().ToString(),
            StatusCodes = StatusCodes.Status500InternalServerError,
            context.HttpContext.Request.Path,
            Message = "Unexpected error. To help with the solution, send the details with the generated token to the administrator."
        };

        _logger.LogError(context.Exception, $"[{exceptionModel.Token}] {exceptionModel.Path} {context.Exception.Message}");

        context.Result = new JsonResult(exceptionModel);

        context.ExceptionHandled = true;
    }
}