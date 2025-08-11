using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YabaTalk.Domain.Entity
{
    public class Message
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required] public string ChatId { get; set; } = default!;
        [ForeignKey(nameof(ChatId))] public Chat Chat { get; set; } = default!;

        [Required] public string SenderUserId { get; set; } = default!;
        [ForeignKey(nameof(SenderUserId))] public User Sender { get; set; } = default!;

        [Required, MaxLength(4000)]
        public string Body { get; set; } = default!;

        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
