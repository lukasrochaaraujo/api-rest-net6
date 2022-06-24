using System.Threading;
using System.Threading.Tasks;
using Rest.Domain.TaskCardContext;
using Rest.Infrastructure.Messaging;

namespace Rest.Application.TaskCardApplication.CreateComment;

public class CreateCommentCommandHandler : MessageHandler<CreateCommentCommand, object>
{
    private readonly ITaskCardRepository _taskCardRepository;

    public CreateCommentCommandHandler(ITaskCardRepository taskCardRepository) : base()
    {
        _taskCardRepository = taskCardRepository;
    }

    public override async Task<MessageResponse<object>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var taskCard = await _taskCardRepository.FindAllByIdAsync(command.TaskId);
        if (taskCard == null)
        {
            AddError($"task {command.TaskId} not exists.");
            return Response();
        }

        taskCard.AddComment(command.Comment, command.UserName);
        await _taskCardRepository.UpdateAsync(taskCard);

        return Response();
    }
}
