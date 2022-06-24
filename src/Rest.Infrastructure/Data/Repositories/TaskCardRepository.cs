using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Rest.Domain.TaskCardContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest.Infrastructure.Data.Repositories;

public class TaskCardRepository : ITaskCardRepository
{
    private readonly IMongoCollection<TaskCard> _collection;

    public TaskCardRepository(IMongoClient client)
    {
        //todo: move configuration to appsettings approach
        _collection = client.GetDatabase("rest-api").GetCollection<TaskCard>("task_card");
    }

    public async Task<TaskCard> FindAllByIdAsync(string id)
    {
        return await _collection.AsQueryable()
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TaskCard>> GetAllByStatusAsync(Status status)
    {
        return await _collection.AsQueryable()
            .Where(e => e.Status == status)
            .ToListAsync();
    }

    public async Task<TaskCard> IncludeAsync(TaskCard taskCard)
    {
        await _collection.InsertOneAsync(taskCard);
        return taskCard;
    }

    public async Task<TaskCard> UpdateAsync(TaskCard taskCard)
    {
        var filterBuilder = Builders<TaskCard>.Filter;

        var filterToFind = filterBuilder.Eq(e => e.Id, taskCard.Id);

        var updatedInfo = Builders<TaskCard>.Update.Set("Comments", taskCard.Comments);

        return await _collection.FindOneAndUpdateAsync(filterToFind, updatedInfo);
    }
}