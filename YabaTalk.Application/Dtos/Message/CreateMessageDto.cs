using MassTransit;

namespace YabaTalk.Application.Dtos.Message
{
    [EntityName("message.test")]
    public class CreateMessageDto
    {
        public string ReceiverPhone { get; set; } = default!;
        public string Body { get; set; } = default!;
    }
    public record MessageCreatedEvent(string Id, string ChatId, string SenderUserId, string Body, DateTimeOffset SentAt);
}
