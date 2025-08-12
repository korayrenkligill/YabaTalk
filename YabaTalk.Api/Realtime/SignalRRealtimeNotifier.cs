using Microsoft.AspNetCore.SignalR;
using YabaTalk.Api.Hubs;
using YabaTalk.Application.Interfaces;
namespace YabaTalk.Api.Realtime
{
    public class SignalRRealtimeNotifier : IRealtimeNotifier
    {
        private readonly IHubContext<ChatHub> _hub;

        public SignalRRealtimeNotifier(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        public Task SendToUserAsync(string userId, string method, object? payload, CancellationToken ct = default)
            => _hub.Clients.Group($"user:{userId}").SendAsync(method, payload, ct);

        public Task SendToUsersAsync(IEnumerable<string> userIds, string method, object? payload, CancellationToken ct = default)
        {
            var groups = userIds.Select(id => $"user:{id}");
            return _hub.Clients.Groups(groups).SendAsync(method, payload, ct);
        }

        public Task SendToGroupAsync(string groupName, string method, object? payload, CancellationToken ct = default)
            => _hub.Clients.Group(groupName).SendAsync(method, payload, ct);

        public Task SendToChatAsync(string chatId, string method, object? payload, CancellationToken ct = default)
    => _hub.Clients.Group($"chat:{chatId}").SendAsync(method, payload, ct);

        public Task SendToChatsAsync(IEnumerable<string> chatIds, string method, object? payload, CancellationToken ct = default)
        {
            var groups = chatIds.Select(id => $"chat:{id}");
            return _hub.Clients.Groups(groups).SendAsync(method, payload, ct);
        }
    }
}
