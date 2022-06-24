using FluentValidation;
using Rest.Infrastructure.Messaging;

namespace Rest.Application.TaskCardApplication.CreateComment;

public class CreateCommentCommand : Message<object>
{
    public string TaskId { get; set; }
    public string Comment { get; set; }
    public string UserName { get; set; }

    public override bool IsValid()
    {
        var validator = new InlineValidator<CreateCommentCommand>();
        validator.RuleFor(c => c.TaskId).NotEmpty();
        validator.RuleFor(c => c.Comment).NotEmpty();
        validator.RuleFor(c => c.UserName).NotEmpty();

        ValidationResult = validator.Validate(this);

        return ValidationResult.IsValid;
    }
}
