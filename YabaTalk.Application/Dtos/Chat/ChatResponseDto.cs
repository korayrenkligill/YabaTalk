using YabaTalk.Application.Dtos.ChatParticipant;
using YabaTalk.Application.Dtos.Message;
using YabaTalk.Domain.Enums;

namespace YabaTalk.Application.Dtos.Chat
{
    public class ChatResponseDto
    {   
        public string Id { get; set; } = default!;
        public ChatType Type { get; set; } = ChatType.Direct;
        public string? Title { get; set; } 
        public string? CreatedByUserId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public ICollection<ChatParticipantResponseDto> Participants { get; set; } = new List<ChatParticipantResponseDto>();
        public ICollection<MessageResponseDto> Messages { get; set; } = new List<MessageResponseDto>();
    }
}
