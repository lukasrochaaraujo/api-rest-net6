using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Rest.Api.Models;
using Rest.Infrastructure.Mediator;
using Rest.Infrastructure.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Api.Controllers;

[ProducesResponseType(typeof(ProblemDetail), StatusCodes.Status500InternalServerError)]
[ProducesResponseType(typeof(Dictionary<string, string[]>), StatusCodes.Status400BadRequest)]
public class ApiControllerBase : ControllerBase
{
    private IMediatorHandler _mediator { get => HttpContext.RequestServices.GetService<IMediatorHandler>(); }

    protected async Task<IActionResult> SendCommand<TResponse>(Message<TResponse> command)
    {
        var commandResult = await _mediator.SendMessageAsync(command);

        if (!commandResult.IsValid)
            return InvalidResponse(commandResult);

        return StatusCode(StatusCodes.Status201Created);
    }

    protected async Task<IActionResult> SendQuery<TResponse>(Message<TResponse> query)
    {
        var queryResult = await _mediator.SendMessageAsync(query);

        if (!queryResult.IsValid)
            return InvalidResponse(queryResult);

        if (queryResult is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return StatusCode(StatusCodes.Status200OK, queryResult.Result);
    }

    private IActionResult InvalidResponse<TResponse>(MessageResponse<TResponse> messageResult)
    {
        return BadRequest(new
        {
            Messages = messageResult.Errors.Select(e => e.ErrorMessage)
        });
    }
}
