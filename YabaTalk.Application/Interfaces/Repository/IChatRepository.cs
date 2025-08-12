using YabaTalk.Domain.Entity;

namespace YabaTalk.Application.Interfaces.Repository
{
    public interface IChatRepository
    {
        Task<Chat> CreateChatAsync(Chat chat, CancellationToken cancellationToken = default);
        Task<Chat?> GetChatByIdAsync(string chatId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Chat>> GetChatsByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Chat>> GetChatsAsync(CancellationToken cancellationToken = default);
        Task<Chat?> FindChatBetweenUsers(string senderId, string receiverId,CancellationToken cancellationToken = default);
    }
}
