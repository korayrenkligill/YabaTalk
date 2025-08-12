using YabaTalk.Domain.Entity;

namespace YabaTalk.Application.Interfaces.Repository
{
    public interface IChatParticipantRepository
    {
        Task<ChatParticipant> CreateChatParticipantAsync(ChatParticipant chatParticipant, CancellationToken cancellationToken = default);
        Task<IEnumerable<ChatParticipant>> GetChatParticipantsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ChatParticipant>> GetChatParticipantsByChatIdAsync(string chatId,CancellationToken cancellationToken = default);
    }
}
