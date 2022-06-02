using MediatR;
using Rest.Domain.TaskCardContext;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rest.Application.TaskCardApplication.GetAllByStatus;

public class GetAllByStatusQueryHandler : IRequestHandler<GetAllByStatusQuery, IEnumerable<TaskCard>>
{
    private readonly ITaskCardRepository _taskCardRepository;

    public GetAllByStatusQueryHandler(ITaskCardRepository taskCardRepository)
    {
        _taskCardRepository = taskCardRepository;
    }

    public async Task<IEnumerable<TaskCard>> Handle(GetAllByStatusQuery query, CancellationToken cancellationToken)
    {
        return await _taskCardRepository.GetAllByStatusAsync(query.Status);
    }
}
