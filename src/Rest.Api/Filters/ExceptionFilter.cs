using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Rest.Api.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

        switch (context.Exception)
        {
            case ValidationException _:
                CreateBadRequest(context);
                break;
            default:
                CreateInternalServerError(context);
                break;
        }      
    }

    private void CreateBadRequest(ExceptionContext context)
    {
        var validationEx = (ValidationException)context.Exception;

        var validationFailureList = validationEx.Errors
            .Select(e => new ValidationFailureDetail
            {
                PropertyName = e.PropertyName,
                Message = e.ErrorMessage
            });

        context.Result = new JsonResult(validationFailureList);
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