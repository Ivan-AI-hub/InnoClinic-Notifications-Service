using AutoMapper;
using NotificationAPI.Application.Abstraction;
using NotificationAPI.Application.Abstraction.Models;
using NotificationAPI.Domain;
using NotificationAPI.Domain.Interfaces;

namespace NotificationAPI.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(UserDTO user, CancellationToken cancellationToken = default)
        {
            var dataUser = _mapper.Map<User>(user);
            await _userRepository.AddAsync(dataUser, cancellationToken);
        }

        public async Task<UserDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _userRepository.RemoveAsync(id, cancellationToken);
        }
    }
}
