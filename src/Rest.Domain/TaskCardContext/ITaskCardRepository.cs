using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest.Domain.TaskCardContext;

public interface ITaskCardRepository
{
    Task<TaskCard> FindAllByIdAsync(string id);
    Task<IEnumerable<TaskCard>> GetAllByStatusAsync(Status status);
    Task<TaskCard> IncludeAsync(TaskCard taskCard);
    Task<TaskCard> UpdateAsync(TaskCard taskCard);
}