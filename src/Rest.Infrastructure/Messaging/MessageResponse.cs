using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Rest.Infrastructure.Messaging
{
    public class MessageResponse<TResult>
    {
        public TResult Result { get; private set; }
        public IEnumerable<ValidationFailure> Errors { get; private set; }
        public bool IsValid => !Errors.Any();

        public MessageResponse(TResult result, ValidationResult validationResult)
        {
            Result = result;
            Errors = validationResult?.Errors ?? Enumerable.Empty<ValidationFailure>();
        }
    }
}
