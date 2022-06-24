using Bogus;
using Moq;
using Rest.Application.TaskCardApplication.GetAllByStatus;
using Rest.Domain.TaskCardContext;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Rest.Tests.Application.TaskCardApplication;

public class GetAllByStatusQueryTest
{
    private readonly Faker _faker;
    private readonly Mock<ITaskCardRepository> _taskCardRepository;
    private readonly GetAllByStatusQueryHandler _queryHandler;

    public GetAllByStatusQueryTest()
    {
        _faker = new Faker();
        _taskCardRepository = new Mock<ITaskCardRepository>();
        _queryHandler = new GetAllByStatusQueryHandler(_taskCardRepository.Object);
    }

    [Fact]
    public async Task GetByRandonStatus_MustReturnCorrespondent()
    {
        //arrange
        Status status = (Status)_faker.Random.Int(0, 3);
        var taskCardsReturn = new List<TaskCard>() { new TaskCard("T", "D", Priority.High) };
        _taskCardRepository.Setup(r => r.GetAllByStatusAsync(It.IsAny<Status>()))
            .ReturnsAsync(taskCardsReturn);

        //act
        var queryResult = await _queryHandler.Handle(new GetAllByStatusQuery { Status = status }, CancellationToken.None);

        //assert
        queryResult.Result.ShouldNotBeEmpty();
        _taskCardRepository.Verify(r => r.GetAllByStatusAsync(It.Is<Status>(s => s == status)), Times.Once);
    }
}
