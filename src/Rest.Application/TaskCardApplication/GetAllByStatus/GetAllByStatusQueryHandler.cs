using Rest.Domain.TaskCardContext;
using Rest.Infrastructure.Messaging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rest.Application.TaskCardApplication.GetAllByStatus;

public class GetAllByStatusQueryHandler : MessageHandler<GetAllByStatusQuery, IEnumerable<TaskCard>>
{
    private readonly ITaskCardRepository _taskCardRepository;

    public GetAllByStatusQueryHandler(ITaskCardRepository taskCardRepository)
    {
        _taskCardRepository = taskCardRepository;
    }

    public override async Task<MessageResponse<IEnumerable<TaskCard>>> Handle(GetAllByStatusQuery command, CancellationToken cancellationToken)
    {
        var result = await _taskCardRepository.GetAllByStatusAsync(command.Status);
        return Response(result);
    }
}
