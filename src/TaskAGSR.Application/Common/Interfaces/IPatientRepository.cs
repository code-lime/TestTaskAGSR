using TaskAGSR.Domain.Entities;

namespace TaskAGSR.Application.Common.Interfaces;

public interface IPatientRepository
{
    Task<Guid> CreateAsync(Patient patient, CancellationToken cancellationToken);
    Task<Patient?> ReadAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, Patient patient, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
