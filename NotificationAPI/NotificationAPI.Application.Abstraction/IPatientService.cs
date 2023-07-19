using NotificationAPI.Application.Abstraction.Models;

namespace NotificationAPI.Application.Abstraction
{
    public interface IPatientService
    {
        public Task AddAsync(PatientDTO patient, CancellationToken cancellationToken = default);
        public Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<PatientDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
