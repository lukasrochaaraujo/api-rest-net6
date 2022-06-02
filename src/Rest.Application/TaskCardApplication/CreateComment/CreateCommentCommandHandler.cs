using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rest.Domain.TaskCardContext;

namespace Rest.Application.TaskCardApplication.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
{
    private readonly ITaskCardRepository _taskCardRepository;

    public CreateCommentCommandHandler(ITaskCardRepository taskCardRepository)
    {
        _taskCardRepository = taskCardRepository;
    }

    public async Task<Unit> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        //todo: validation
        var taskCard = await _taskCardRepository.FindAllByIdAsync(command.TaskId);
        if (taskCard != null)
        {
            taskCard.AddComment(command.Comment, command.UserName);
            await _taskCardRepository.UpdateAsync(taskCard);
        }
        return Unit.Value;
    }
}
