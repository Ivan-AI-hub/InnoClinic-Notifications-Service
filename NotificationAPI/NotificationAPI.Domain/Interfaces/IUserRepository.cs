namespace NotificationAPI.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task AddAsync(User user, CancellationToken cancellationToken = default);
        public Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
