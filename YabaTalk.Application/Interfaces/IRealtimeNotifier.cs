
namespace YabaTalk.Application.Interfaces
{
    public interface IRealtimeNotifier
    {
        Task SendToUserAsync(string userId, string method, object? payload, CancellationToken ct = default);
        Task SendToUsersAsync(IEnumerable<string> userIds, string method, object? payload, CancellationToken ct = default);
        Task SendToGroupAsync(string groupName, string method, object? payload, CancellationToken ct = default);
        Task SendToChatAsync(string chatId, string method, object? payload, CancellationToken ct = default);
        Task SendToChatsAsync(IEnumerable<string> chatIds, string method, object? payload, CancellationToken ct = default);
    }
}
