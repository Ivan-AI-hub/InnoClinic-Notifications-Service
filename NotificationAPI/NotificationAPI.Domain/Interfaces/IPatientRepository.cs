namespace NotificationAPI.Domain.Interfaces
{
    public interface IPatientRepository
    {
        public Task AddAsync(Patient patient, CancellationToken cancellationToken = default);
        public Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<Patient> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
