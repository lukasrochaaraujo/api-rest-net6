using Rest.Infrastructure.Messaging;
using System.Threading.Tasks;

namespace Rest.Infrastructure.Mediator
{
    public interface IMediatorHandler
    {
        Task<MessageResponse<TResult>> SendMessageAsync<TResult>(Message<TResult> message);
    }
}
