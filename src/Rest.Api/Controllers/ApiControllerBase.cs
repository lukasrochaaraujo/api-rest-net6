using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Rest.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest.Api.Controllers;

[ProducesResponseType(typeof(ProblemDetail), StatusCodes.Status500InternalServerError)]
[ProducesResponseType(typeof(IEnumerable<ValidationFailureDetail>), StatusCodes.Status400BadRequest)]
public class ApiControllerBase : ControllerBase
{
    private ISender _mediator { get => HttpContext.RequestServices.GetService<ISender>(); }

    protected async Task<IActionResult> SendCommand<TResponse>(IRequest<TResponse> command)
    {
        var commandResult = await _mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, commandResult);
    }

    protected async Task<IActionResult> SendQuery<TResponse>(IRequest<TResponse> query)
    {
        var queryResult = await _mediator.Send(query);
        
        if (queryResult is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return StatusCode(StatusCodes.Status200OK, queryResult);
    }
}
