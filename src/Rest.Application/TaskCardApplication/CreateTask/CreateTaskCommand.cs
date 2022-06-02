using MediatR;
using Rest.Domain.TaskCardContext;

namespace Rest.Application.TaskCardApplication.CreateTask;

public class CreateTaskCommand : IRequest<TaskCard>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
}
