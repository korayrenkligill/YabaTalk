using YabaTalk.Domain.Entity;

namespace YabaTalk.Application.Interfaces.Repository
{
    public interface IMessageRepository
    {
        Task<Message> CreateMessageAsync(Message message, CancellationToken cancellationToken = default);
        Task<IEnumerable<Message>> GetMessagesByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Message>> GetMessagesAsync(CancellationToken cancellationToken = default);
    }
}
