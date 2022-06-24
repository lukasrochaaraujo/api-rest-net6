using System.Threading;
using System.Threading.Tasks;
using Rest.Domain.TaskCardContext;
using Rest.Infrastructure.Messaging;

namespace Rest.Application.TaskCardApplication.CreateTask;

public class CreateTaskCommandHandler : MessageHandler<CreateTaskCommand, TaskCard>
{
    private readonly ITaskCardRepository _taskCardRepository;

    public CreateTaskCommandHandler(ITaskCardRepository taskCardRepository) : base()
    {
        _taskCardRepository = taskCardRepository;
    }

    public override async Task<MessageResponse<TaskCard>> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var taskCard = new TaskCard(command.Title, command.Description, command.Priority);
        taskCard = await _taskCardRepository.IncludeAsync(taskCard);
        return Response(taskCard);
    }
}
