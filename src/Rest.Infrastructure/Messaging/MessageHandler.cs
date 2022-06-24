using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Rest.Infrastructure.Messaging
{
    public abstract class MessageHandler<TCommand, TResult>
        : IRequestHandler<TCommand, MessageResponse<TResult>> where TCommand : Message<TResult>
    {
        private readonly ValidationResult _validationResult;
        public bool IsValid => _validationResult.IsValid;

        protected MessageHandler()
        {
            _validationResult = new ValidationResult();
        }

        public abstract Task<MessageResponse<TResult>> Handle(TCommand command, CancellationToken cancellationToken);

        protected void AddError(string message)
            => _validationResult.Errors.Add(new ValidationFailure(string.Empty, message));

        protected MessageResponse<TResult> Response()
            => new(default, _validationResult);

        protected MessageResponse<TResult> Response(TResult result)
            => new(result, _validationResult);

        protected MessageResponse<TResult> Response(ValidationResult validationResult)
            => new(default, validationResult);
    }
}
