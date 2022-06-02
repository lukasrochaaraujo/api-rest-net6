using Bogus;
using Rest.Domain.TaskCardContext;
using Shouldly;
using System;
using Xunit;

namespace Rest.Tests.Domain.TaskCardContext;

public class TaskCardTest
{
    private readonly Faker _faker;

    public TaskCardTest()
    {
        _faker = new Faker();
    }

    [Fact]
    public void CreatedUsingConstructor_MustCreateAValidTask()
    {
        //arrange
        string title = _faker.Lorem.Sentence();
        string description = _faker.Lorem.Text();
        Priority priority = (Priority)_faker.Random.Int(0, 3);

        //act
        var taskCard = new TaskCard(title, description, priority);

        //assert
        taskCard.Title.ShouldBe(title);
        taskCard.Description.ShouldBe(description);
        taskCard.Status.ShouldBe(Status.Todo);
        taskCard.Priority.ShouldBe(priority);
        taskCard.Created.ShouldBeGreaterThan(DateTime.MinValue);
        taskCard.Comments.ShouldBeEmpty();
    }

    [Fact]
    public void StartTaskInTodo_MustChangeStatusToInProgressAndSetStartedDate()
    {
        //arrange
        string title = _faker.Lorem.Sentence();
        string description = _faker.Lorem.Text();
        Priority priority = (Priority)_faker.Random.Int(0, 3);

        //act
        var taskCard = new TaskCard(title, description, priority);
        taskCard.Start();

        //assert
        taskCard.Status.ShouldBe(Status.InProgress);
        taskCard.Started.ShouldNotBeNull().ShouldBeGreaterThan(DateTime.MinValue);
    }

    [Fact]
    public void StartTaskCancelled_MustNotStartAgain()
    {
        //arrange
        string title = _faker.Lorem.Sentence();
        string description = _faker.Lorem.Text();
        Priority priority = (Priority)_faker.Random.Int(0, 3);

        //act
        var taskCard = new TaskCard(title, description, priority);
        taskCard.Start();
        taskCard.Cancel("try", "some one");

        Status cancelledStatus = taskCard.Status;
        DateTime cancelledStarted = taskCard.Started.Value;

        taskCard.Start();

        //assert
        taskCard.Status.ShouldBe(cancelledStatus);
        taskCard.Started.ShouldNotBeNull().ShouldBe(cancelledStarted);
    }

    [Fact]
    public void StartTaskDone_MustNotStartAgain()
    {
        //arrange
        string title = _faker.Lorem.Sentence();
        string description = _faker.Lorem.Text();
        Priority priority = (Priority)_faker.Random.Int(0, 3);

        //act
        var taskCard = new TaskCard(title, description, priority);
        taskCard.Start();
        taskCard.Done();

        Status donetatus = taskCard.Status;
        DateTime doneStarted = taskCard.Started.Value;

        taskCard.Start();

        //assert
        taskCard.Status.ShouldBe(donetatus);
        taskCard.Started.ShouldNotBeNull().ShouldBe(doneStarted);
    }

    [Fact]
    public void CancelTaskInProgress_MustChangeStatusToCancelledAndSetFinishedDateWithComment()
    {
        //arrange
        string title = _faker.Lorem.Sentence();
        string description = _faker.Lorem.Text();
        Priority priority = (Priority)_faker.Random.Int(0, 3);
        string cancelComment = _faker.Lorem.Sentence();
        string cancelCommentedBy = _faker.Person.FirstName;

        //act
        var taskCard = new TaskCard(title, description, priority);
        taskCard.Start();
        taskCard.Cancel(cancelComment, cancelCommentedBy);

        //assert
        taskCard.Status.ShouldBe(Status.Cancelled);
        taskCard.Finished.ShouldNotBeNull().ShouldBeGreaterThan(DateTime.MinValue);
        taskCard.Comments.ShouldNotBeEmpty();
    }

    [Fact]
    public void AddComment_MustAddNewCommentOnCommentsList()
    {
        //arrange
        string title = _faker.Lorem.Sentence();
        string description = _faker.Lorem.Text();
        Priority priority = (Priority)_faker.Random.Int(0, 3);
        string cancelComment = _faker.Lorem.Sentence();
        string cancelCommentedBy = _faker.Person.FirstName;

        //act
        var taskCard = new TaskCard(title, description, priority);
        taskCard.Start();
        taskCard.AddComment(cancelComment, cancelCommentedBy);

        //assert
        taskCard.Comments.ShouldNotBeEmpty();
        taskCard.Comments.ShouldNotBeEmpty();
        taskCard.Comments.Count.ShouldBe(1);
    }
}
