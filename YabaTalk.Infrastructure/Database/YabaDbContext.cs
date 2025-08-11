using Microsoft.EntityFrameworkCore;
using YabaTalk.Domain.Entity;

namespace YabaTalk.Infrastructure.Database
{
    public class YabaDbContext : DbContext
    {
        public YabaDbContext(DbContextOptions<YabaDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Chat> Chats => Set<Chat>();
        public DbSet<ChatParticipant> ChatParticipants => Set<ChatParticipant>();
        public DbSet<Message> Messages => Set<Message>();


        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            b.Entity<ChatParticipant>()
             .HasKey(cp => new { cp.ChatId, cp.UserId });

            b.Entity<Chat>()
             .HasIndex(c => c.Type);

            b.Entity<Message>()
             .HasOne(m => m.Chat)
             .WithMany(c => c.Messages)
             .HasForeignKey(m => m.ChatId)
             .OnDelete(DeleteBehavior.Cascade);

            b.Entity<Message>()
             .HasOne(m => m.Sender)
             .WithMany(u => u.MessagesSent)
             .HasForeignKey(m => m.SenderUserId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
