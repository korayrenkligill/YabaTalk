using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YabaTalk.Domain.Entity
{
    public class ChatParticipant
    {
        [Required] public string ChatId { get; set; } = default!;
        [ForeignKey(nameof(ChatId))] public Chat Chat { get; set; } = default!;

        [Required] public string UserId { get; set; } = default!;
        [ForeignKey(nameof(UserId))] public User User { get; set; } = default!;

        public bool IsAdmin { get; set; } = false;

        public DateTimeOffset JoinedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
