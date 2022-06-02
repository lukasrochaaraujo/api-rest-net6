using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rest.Domain.TaskCardContext;

namespace Rest.Application.TaskCardApplication.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskCard>
{
    private readonly ITaskCardRepository _taskCardRepository;

    public CreateTaskCommandHandler(ITaskCardRepository taskCardRepository)
    {
        _taskCardRepository = taskCardRepository;
    }

    public async Task<TaskCard> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        //todo: validation
        var taskCard = new TaskCard(command.Title, command.Description, command.Priority);
        taskCard = await _taskCardRepository.IncludeAsync(taskCard);
        return taskCard;
    }
}
