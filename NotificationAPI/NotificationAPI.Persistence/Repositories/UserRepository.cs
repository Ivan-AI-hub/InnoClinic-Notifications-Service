using Microsoft.EntityFrameworkCore;
using NotificationAPI.Domain;
using NotificationAPI.Domain.Exceptions;
using NotificationAPI.Domain.Interfaces;

namespace NotificationAPI.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NotificationContext _context;

        public UserRepository(NotificationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }
            return user;
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
