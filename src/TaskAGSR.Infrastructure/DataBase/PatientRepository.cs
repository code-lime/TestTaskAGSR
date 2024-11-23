using Hl7.Fhir.Search;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskAGSR.Application.Common.Interfaces;
using TaskAGSR.Domain.Entities;
using TaskAGSR.Infrastructure.Extensions;

namespace TaskAGSR.Infrastructure.DataBase;

public class PatientRepository(
    ApplicationDbContext context) :
    IPatientRepository
{
    public async Task<Guid> CreateAsync(Patient patient, CancellationToken cancellationToken)
    {
        var entry = await context.Patients.AddAsync(patient, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task<Patient?> ReadAsync(Guid id, CancellationToken cancellationToken) 
        => await context.Patients.FindAsync([id], cancellationToken);

    public async Task<bool> UpdateAsync(Guid id, Patient patient, CancellationToken cancellationToken)
        => await context.Patients
            .Where(v => v.Id == id)
            .ExecuteUpdateAsync(v => v
                .SetProperty(v => v.Use, patient.Use)
                .SetProperty(v => v.Family, patient.Family)
                .SetProperty(v => v.Given, patient.Given)
                .SetProperty(v => v.Gender, patient.Gender)
                .SetProperty(v => v.BirthDate, patient.BirthDate)
                .SetProperty(v => v.Active, patient.Active),
                cancellationToken) > 0;

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        => await context.Patients
            .Where(v => v.Id == id)
            .ExecuteDeleteAsync(cancellationToken) > 0;

    public async Task<IEnumerable<Patient>> SearchByBirthDateAsync(IEnumerable<ISearch<DateTime>> filter, CancellationToken cancellationToken) 
        => await context.Patients
            .Where(filter.Select(v => v.SearchExpression).CombineFilter(false).Map<Patient, DateTime, bool>(v => v.BirthDate))
            .ToListAsync(cancellationToken);
}