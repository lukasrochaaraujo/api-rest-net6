using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rest.Application.TaskCardApplication.CreateComment;
using Rest.Application.TaskCardApplication.CreateTask;
using System.Net;
using System.Threading.Tasks;

namespace Rest.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TaskCardController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskCardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PostTask([FromBody] CreateTaskCommand createTaskCommand)
    {
        var createdTask = await _mediator.Send(createTaskCommand);
        return Ok(createdTask);
    }

    [HttpPost("{taskId}/comment")]
    public async Task<IActionResult> PostTaskComment([FromRoute] string taskId, [FromBody] CreateCommentCommand createCommentCommand)
    {
        createCommentCommand.TaskId = taskId;        
        var createdTask = await _mediator.Send(createCommentCommand);
        return StatusCode((int)HttpStatusCode.Created);
    }
}