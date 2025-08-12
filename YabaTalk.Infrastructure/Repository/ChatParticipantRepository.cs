using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YabaTalk.Application.Interfaces.Repository;
using YabaTalk.Domain.Entity;
using YabaTalk.Infrastructure.Database;

namespace YabaTalk.Infrastructure.Repository
{
    public class ChatParticipantRepository : IChatParticipantRepository
    {
        private readonly YabaDbContext _context;

        public ChatParticipantRepository(YabaDbContext context)
        {
            _context = context;
        }

        /// Yeni bir chat katılımcısı oluşturur.
        public async Task<ChatParticipant> CreateChatParticipantAsync(ChatParticipant chatParticipant, CancellationToken cancellationToken = default)
        {
            await _context.ChatParticipants.AddAsync(chatParticipant, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return chatParticipant;
        }

        /// Tüm chatlerim tüm katılımcılarını getirir.
        public async Task<IEnumerable<ChatParticipant>> GetChatParticipantsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ChatParticipants
              .Include(c => c.Chat)
              .Include(c => c.User)
              .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ChatParticipant>> GetChatParticipantsByChatIdAsync(string chatId, CancellationToken cancellationToken = default)
        {
            return await _context.ChatParticipants.Where(c => c.ChatId == chatId)
              .Include(c => c.Chat)
              .Include(c => c.User)
              .ToListAsync(cancellationToken);
        }
    }
}
