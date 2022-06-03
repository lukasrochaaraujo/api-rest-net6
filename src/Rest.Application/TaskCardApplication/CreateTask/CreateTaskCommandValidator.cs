using FluentValidation;

namespace Rest.Application.TaskCardApplication.CreateTask;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty();

        RuleFor(c => c.Description)
            .NotEmpty();            
    }
}
