using MediatR;
using Rest.Domain.TaskCardContext;
using System.Collections.Generic;

namespace Rest.Application.TaskCardApplication.GetAllByStatus;

public class GetAllByStatusQuery : IRequest<IEnumerable<TaskCard>>
{
    public Status Status { get; set; }
}