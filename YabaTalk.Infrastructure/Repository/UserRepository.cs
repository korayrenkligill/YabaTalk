using Microsoft.EntityFrameworkCore;
using YabaTalk.Application.Interfaces.Repository;
using YabaTalk.Domain.Entity;
using YabaTalk.Infrastructure.Database;

namespace YabaTalk.Infrastructure.Repository
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly YabaDbContext _context;

        public UserRepository(YabaDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
            => await _context.Users.FindAsync(new object?[] { id }, cancellationToken);

        public async Task<User?> GetByPhoneAsync(string phoneE164, CancellationToken cancellationToken = default)
            => await _context.Users.FirstOrDefaultAsync(u => u.PhoneE164 == phoneE164, cancellationToken);

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Users.ToListAsync(cancellationToken);

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Users.FindAsync(new object?[] { id }, cancellationToken);
            if (entity is not null)
            {
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
