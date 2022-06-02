using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rest.Application.TaskCardApplication.CreateComment;
using Rest.Application.TaskCardApplication.CreateTask;
using Rest.Application.TaskCardApplication.GetAllByStatus;
using Rest.Domain.TaskCardContext;
using System.Net;
using System.Threading.Tasks;

namespace Rest.Api.Controllers;

[ApiController]
[Route("tasks")]
[ApiVersion("1")]
public class TaskCardController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskCardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByStatusAsync([FromQuery] GetAllByStatusQuery query)
    {
        var result = await _mediator.Send(query); ;
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TaskCard), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> PostTaskAsync([FromBody] CreateTaskCommand createTaskCommand)
    {
        var createdTask = await _mediator.Send(createTaskCommand);
        return Ok(createdTask);
    }

    [HttpPost("{taskId}/comment")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> PostTaskCommentAsync([FromRoute] string taskId, [FromBody] CreateCommentCommand createCommentCommand)
    {
        createCommentCommand.TaskId = taskId;        
        await _mediator.Send(createCommentCommand);
        return StatusCode((int)HttpStatusCode.Created);
    }
}