using YabaTalk.Domain.Enums;

namespace YabaTalk.Application.Dtos.Message
{
    public class MessageResponseDto
    {
        public string Id { get; set; } = default!;
        public string ChatId { get; set; } = default!;
        public string SenderUserId { get; set; } = default!;
        public string Body { get; set; } = default!;
        public DateTimeOffset SentAt { get; set; }
        public MessageStatus Status { get; set; } = MessageStatus.Sent;
    }
}
