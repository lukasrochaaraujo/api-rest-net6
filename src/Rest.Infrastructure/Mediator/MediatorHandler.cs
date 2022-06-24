using MediatR;
using Rest.Infrastructure.Messaging;
using System.Threading.Tasks;

namespace Rest.Infrastructure.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<MessageResponse<TResult>> SendMessageAsync<TResult>(Message<TResult> message)
        {
            if (!message.IsValid()) 
                return new(default, message.ValidationResult);

            return await _mediator.Send(message).ConfigureAwait(false);
        }
    }
}
