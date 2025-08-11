using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICollection<ChatParticipant> Chats { get; set; } = new List<ChatParticipant>();
        public ICollection<Message> MessagesSent { get; set; } = new List<Message>();
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
