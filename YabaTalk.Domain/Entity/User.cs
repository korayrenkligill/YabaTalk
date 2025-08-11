using System.ComponentModel.DataAnnotations;


namespace YabaTalk.Domain.Entity
{
    public class User
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = default!;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = default!;

        [MaxLength(20)] public string PhoneE164 { get; set; } = default!;
        public string? AvatarUrl { get; set; }
        [MaxLength(160)] public string? About { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public ICollection<ChatParticipant> Chats { get; set; } = new List<ChatParticipant>();
        public ICollection<Message> MessagesSent { get; set; } = new List<Message>();
    }
}
