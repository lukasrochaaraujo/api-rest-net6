using FluentValidation;
using Rest.Domain.TaskCardContext;
using Rest.Infrastructure.Messaging;

namespace Rest.Application.TaskCardApplication.CreateTask;

public class CreateTaskCommand : Message<TaskCard>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }

    public override bool IsValid()
    {
        var validator = new InlineValidator<CreateTaskCommand>();
        validator.RuleFor(c => c.Title).NotEmpty();
        validator.RuleFor(c => c.Description).NotEmpty();

        ValidationResult = validator.Validate(this);

        return ValidationResult.IsValid;
    }
}
