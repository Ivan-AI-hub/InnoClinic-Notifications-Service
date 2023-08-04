using Microsoft.EntityFrameworkCore;
using NotificationAPI.Domain;
using NotificationAPI.Domain.Exceptions;
using NotificationAPI.Domain.Interfaces;

namespace NotificationAPI.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly NotificationContext _context;

        public PatientRepository(NotificationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Patient patient, CancellationToken cancellationToken = default)
        {
            await _context.Patients.AddAsync(patient, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Patient> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }
            return patient;
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
