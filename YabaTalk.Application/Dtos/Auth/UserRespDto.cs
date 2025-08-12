using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YabaTalk.Application.Dtos.ChatParticipant;
using YabaTalk.Application.Dtos.Message;
using YabaTalk.Domain.Entity;

namespace YabaTalk.Application.Dtos.Auth
{
    public class UserRespDto
    {
        public string Id { get; set; } = default!;
        public string AccessToken { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneE164 { get; set; } = default!;
        public string? AvatarUrl { get; set; }
        public string? About { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public ICollection<ChatParticipantResponseDto> Chats { get; set; } = new List<ChatParticipantResponseDto>();
        public ICollection<MessageResponseDto> MessagesSent { get; set; } = new List<MessageResponseDto>();
    }

    public class UserRespWithoutDetailDto
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneE164 { get; set; } = default!;
        public string? AvatarUrl { get; set; }
        public string? About { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
