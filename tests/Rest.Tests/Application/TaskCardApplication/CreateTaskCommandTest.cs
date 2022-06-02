using Bogus;
using MediatR;
using Moq;
using Rest.Application.TaskCardApplication.CreateTask;
using Rest.Domain.TaskCardContext;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Rest.Tests.Application.TaskCardApplication
{
    public class CreateTaskCommandTest
    {
        private readonly Faker _faker;
        private readonly Mock<ITaskCardRepository> _taskCardRepository;
        private readonly IRequestHandler<CreateTaskCommand, TaskCard> _commandHandler;

        public CreateTaskCommandTest()
        {
            _faker = new Faker();
            _taskCardRepository = new Mock<ITaskCardRepository>();
            _commandHandler = new CreateTaskCommandHandler(_taskCardRepository.Object);
        }

        [Fact]
        public async Task ValidCreateCommand_MustCreateNewTaskCard()
        {
            //arrange
            var command = new CreateTaskCommand
            {
                Title = _faker.Lorem.Sentence(),
                Description = _faker.Lorem.Text(),
                Priority = (Priority)_faker.Random.Int(0,3)
            };
            _taskCardRepository.Setup(r => r.IncludeAsync(It.IsAny<TaskCard>()))
                .ReturnsAsync(new TaskCard(command.Title, command.Description, command.Priority));

            //act
            var createdTaskCard = await _commandHandler.Handle(command, new CancellationToken());

            //assert
            _taskCardRepository.Verify(r => r.IncludeAsync(It.IsAny<TaskCard>()), Times.Once);
            createdTaskCard.ShouldNotBeNull();
            createdTaskCard.Title.ShouldBe(command.Title);
            createdTaskCard.Description.ShouldBe(command.Description);
            createdTaskCard.Priority.ShouldBe(command.Priority);
            createdTaskCard.Status.ShouldBe(Status.Todo);
            createdTaskCard.Created.ShouldBeGreaterThan(DateTime.MinValue);
            createdTaskCard.Comments.ShouldBeEmpty();
        }
    }
}
