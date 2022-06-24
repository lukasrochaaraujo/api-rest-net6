using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace Rest.Infrastructure.Messaging
{
    public abstract class Message<TResult> : IRequest<MessageResponse<TResult>>
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid() => true;
    }
}
