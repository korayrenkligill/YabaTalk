using MassTransit;
using System.Diagnostics;
using YabaTalk.Application.Dtos.Message;
using YabaTalk.Application.Interfaces;
using YabaTalk.Application.Interfaces.Service;

namespace YabaTalk.Infrastructure.Consumer
{
    public class CreateMessageDtoConsumer : IConsumer<MessageCreatedEvent>
    {
        private readonly IMessageService _ms;
        private readonly IRealtimeNotifier _realtime;
        public CreateMessageDtoConsumer(IMessageService ms, IRealtimeNotifier realtime)
        {
            _ms = ms;
            _realtime = realtime;
        }

        public async Task Consume(ConsumeContext<MessageCreatedEvent> ctx)
        {
            var m = ctx.Message;
            await _realtime.SendToChatAsync(m.ChatId, "NewMessage", m, ctx.CancellationToken);
            await _realtime.SendToUserAsync(m.SenderUserId, "MessageSent", new { chatId = m.ChatId, id = m.Id }, ctx.CancellationToken);
        }
    }
}
