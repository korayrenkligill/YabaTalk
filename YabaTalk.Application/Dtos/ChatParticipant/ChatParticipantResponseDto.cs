using YabaTalk.Application.Dtos.Auth;

namespace YabaTalk.Application.Dtos.ChatParticipant
{
    public class ChatParticipantResponseDto
    {
        public UserRespWithoutDetailDto User { get; set; } = default!;
        public bool IsAdmin { get; set; } = false;
        public DateTimeOffset JoinedAt { get; set; }
    }
}
