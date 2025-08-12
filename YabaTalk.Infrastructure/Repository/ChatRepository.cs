using Microsoft.EntityFrameworkCore;
using YabaTalk.Application.Interfaces.Repository;
using YabaTalk.Domain.Entity;
using YabaTalk.Infrastructure.Database;

namespace YabaTalk.Infrastructure.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly YabaDbContext _context;

        public ChatRepository(YabaDbContext context)
        {
            _context = context;
        }

        /// Yeni bir sohbet oluşturur.
        public async Task<Chat> CreateChatAsync(Chat chat, CancellationToken cancellationToken = default)
        {
            await _context.Chats.AddAsync(chat, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return chat;
        }

        /// ID'ye göre sohbeti getirir.
        public async Task<Chat?> GetChatByIdAsync(string chatId, CancellationToken cancellationToken = default)
        {
            return await _context.Chats
                .Include(c => c.Participants)
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == chatId, cancellationToken);
        }

        /// Bir kullanıcıya ait tüm sohbetleri getirir.
        public async Task<IEnumerable<Chat>> GetChatsByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _context.ChatParticipants
                .Where(cp => cp.UserId == userId)
                .Select(cp => cp.Chat)
                .Include(c => c.Participants)
                .ToListAsync(cancellationToken);
        }

        /// Tüm sohbetleri getirir.
        public async Task<IEnumerable<Chat>> GetChatsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Chats
                .Include(c => c.Participants)
                .ToListAsync(cancellationToken);
        }

        /// İki kullanıcı arasında var olan bir sohbeti bulur.
        public async Task<Chat?> FindChatBetweenUsers(string senderId, string receiverId, CancellationToken cancellationToken = default)
        {
            // Önce göndericiye ait sohbetleri buluruz.
            var senderChats = await _context.ChatParticipants
                .Where(cp => cp.UserId == senderId)
                .Select(cp => cp.ChatId)
                .ToListAsync(cancellationToken);

            // Bu sohbetlerden alıcının da bulunduğu sohbeti buluruz.
            var chat = await _context.ChatParticipants
                .Where(cp => cp.UserId == receiverId && senderChats.Contains(cp.ChatId))
                .Select(cp => cp.Chat)
                .FirstOrDefaultAsync(cancellationToken);

            return chat;
        }
    }
}
