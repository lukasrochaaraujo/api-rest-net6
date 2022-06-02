using Bogus;
using MediatR;
using Moq;
using Rest.Application.TaskCardApplication.CreateComment;
using Rest.Domain.TaskCardContext;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Rest.Tests.Application.TaskCardApplication
{
    public class CreateCommentCommandTest
    {
        private readonly Faker _faker;
        private readonly Mock<ITaskCardRepository> _taskCardRepository;
        private readonly IRequestHandler<CreateCommentCommand> _commandHandler;

        public CreateCommentCommandTest()
        {
            _faker = new Faker();
            _taskCardRepository = new Mock<ITaskCardRepository>();
            _commandHandler = new CreateCommentCommandHandler(_taskCardRepository.Object);
        }

        [Fact]
        public async Task ValidCreateCommandWithExistingTask_MustAddNewComment()
        {
            //arrange
            var command = new CreateCommentCommand
            {
                TaskId = _faker.Random.Uuid().ToString(),
                Comment = _faker.Lorem.Text(),
                UserName = _faker.Person.FirstName
            };
            var targetTaskCard = new TaskCard(_faker.Lorem.Sentence(), _faker.Lorem.Text(), Priority.Medium);
            _taskCardRepository.Setup(r => r.FindAllByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(targetTaskCard);

            //act
            await _commandHandler.Handle(command, new CancellationToken());

            //assert
            _taskCardRepository.Verify(r => r.FindAllByIdAsync(It.IsAny<string>()), Times.Once);
            _taskCardRepository.Verify(r => r.UpdateAsync(It.IsAny<TaskCard>()), Times.Once);
            targetTaskCard.Comments.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task ValidCreateCommandWithNoExistingTask_MustNotAddNewComment()
        {
            //arrange
            var command = new CreateCommentCommand
            {
                TaskId = _faker.Random.Uuid().ToString(),
                Comment = _faker.Lorem.Text(),
                UserName = _faker.Person.FirstName
            };
            var targetTaskCard = new TaskCard(_faker.Lorem.Sentence(), _faker.Lorem.Text(), Priority.Medium);

            //act
            await _commandHandler.Handle(command, new CancellationToken());

            //assert
            _taskCardRepository.Verify(r => r.FindAllByIdAsync(It.IsAny<string>()), Times.Once);
            _taskCardRepository.Verify(r => r.UpdateAsync(It.IsAny<TaskCard>()), Times.Never);
            targetTaskCard.Comments.ShouldBeEmpty();
        }
    }
}
