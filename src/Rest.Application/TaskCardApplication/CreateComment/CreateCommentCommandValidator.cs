using FluentValidation;

namespace Rest.Application.TaskCardApplication.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(c => c.TaskId)
            .NotEmpty();

        RuleFor(c => c.Comment)
            .NotEmpty();

        RuleFor(c => c.UserName)
            .NotEmpty();
    }
}
