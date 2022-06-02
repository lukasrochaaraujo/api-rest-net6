using MediatR;

namespace Rest.Application.TaskCardApplication.CreateComment;

public class CreateCommentCommand : IRequest
{
    public string TaskId { get; set; }
    public string Comment { get; set; }
    public string UserName { get; set; }
}
