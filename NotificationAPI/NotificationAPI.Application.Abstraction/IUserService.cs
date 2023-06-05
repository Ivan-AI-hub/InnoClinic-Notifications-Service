using NotificationAPI.Application.Abstraction.Models;

namespace NotificationAPI.Application.Abstraction
{
    public interface IUserService
    {
        public Task AddAsync(UserDTO user, CancellationToken cancellationToken = default);
        public Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<UserDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
