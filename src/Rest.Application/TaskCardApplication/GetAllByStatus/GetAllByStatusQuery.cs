using Rest.Domain.TaskCardContext;
using Rest.Infrastructure.Messaging;
using System.Collections.Generic;

namespace Rest.Application.TaskCardApplication.GetAllByStatus;

public class GetAllByStatusQuery : Message<IEnumerable<TaskCard>>
{
    public Status Status { get; set; }
}
