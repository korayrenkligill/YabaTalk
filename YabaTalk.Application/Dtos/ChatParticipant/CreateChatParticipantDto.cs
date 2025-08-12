namespace YabaTalk.Application.Dtos.ChatParticipant
{
    public class CreateChatParticipantDto
    {
        public string ChatId { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public bool IsAdmin { get; set; } = false;
    }
}
