using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rest.Application.TaskCardApplication.CreateComment;
using Rest.Application.TaskCardApplication.CreateTask;
using Rest.Application.TaskCardApplication.GetAllByStatus;
using Rest.Domain.TaskCardContext;
using System.Threading.Tasks;

namespace Rest.Api.Controllers;

[ApiController]
[Route("tasks")]
[ApiVersion("1")]
public class TaskCardController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(TaskCard), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByStatusAsync([FromQuery] GetAllByStatusQuery query)
        => await SendQuery(query);

    [HttpPost]
    [ProducesResponseType(typeof(TaskCard), StatusCodes.Status201Created)]
    public async Task<IActionResult> PostTaskAsync([FromBody] CreateTaskCommand createTaskCommand)
        => await SendCommand(createTaskCommand);

    [HttpPost("{taskId}/comment")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostTaskCommentAsync([FromRoute] string taskId, [FromBody] CreateCommentCommand createCommentCommand)
    {
        createCommentCommand.TaskId = taskId;        
        return await SendCommand(createCommentCommand);
    }
}