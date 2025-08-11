

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YabaTalk.Domain.Enums;

namespace YabaTalk.Domain.Entity
{
    public class Chat
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required] public ChatType Type { get; set; } = ChatType.Direct;

        [MaxLength(120)] public string? Title { get; set; }

        public string? CreatedByUserId { get; set; }
        [ForeignKey(nameof(CreatedByUserId))] public User? CreatedBy { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public ICollection<ChatParticipant> Participants { get; set; } = new List<ChatParticipant>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
