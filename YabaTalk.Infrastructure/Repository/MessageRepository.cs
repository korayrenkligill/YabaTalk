using Microsoft.EntityFrameworkCore;
using System;
using YabaTalk.Application.Interfaces.Repository;
using YabaTalk.Domain.Entity;
using YabaTalk.Infrastructure.Database;

namespace YabaTalk.Infrastructure.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly YabaDbContext _context;

        public MessageRepository(YabaDbContext context)
        {
            _context = context;
        }

        public async Task<Message> CreateMessageAsync(Message message, CancellationToken cancellationToken = default)
        {
            await _context.Messages.AddAsync(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return message;
        }
        public async Task<IEnumerable<Message>> GetMessagesByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Messages.Where(m => m.SenderUserId == userId)
              .ToListAsync(cancellationToken);

        }
        public async Task<IEnumerable<Message>> GetMessagesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Messages
           .ToListAsync(cancellationToken);
        }
    }
}
