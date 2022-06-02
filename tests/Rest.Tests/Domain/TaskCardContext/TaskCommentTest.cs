using Bogus;
using Rest.Domain.TaskCardContext;
using Shouldly;
using System;
using Xunit;

namespace Rest.Tests.Domain.TaskCardContext;

public class TaskCommentTest
{
    [Fact]
    public void CreatedUsingConstructor_MustCreateAValidComment()
    {
        //arrange
        var faker = new Faker();
        string comment = faker.Lorem.Sentence();
        string commentBy = faker.Person.FirstName;

        //act
        var taskComment = new TaskComment(comment, commentBy);

        //assert
        taskComment.Created.ShouldBeGreaterThan(DateTime.MinValue);
        taskComment.Comment.ShouldBe(comment);
        taskComment.CommentedBy.ShouldBe(commentBy);
    }
}