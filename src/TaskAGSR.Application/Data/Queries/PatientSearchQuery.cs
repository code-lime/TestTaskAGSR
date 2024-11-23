using Hl7.Fhir.Search;
using MediatR;
using TaskAGSR.Application.Common.Interfaces;
using TaskAGSR.Application.Common.VM;
using TaskAGSR.Domain.Entities;

namespace TaskAGSR.Application.Data.Queries;

public record PatientSearchByBirthDateQuery(IEnumerable<ISearch<DateTime>> Filter) : IRequest<IEnumerable<PatientVM<PatientIndexIdentityVM>>>;

public class PatientSearchQueryHandler(
    IPatientRepository patientRepository) :
    IRequestHandler<PatientSearchByBirthDateQuery, IEnumerable<PatientVM<PatientIndexIdentityVM>>>
{
    public async Task<IEnumerable<PatientVM<PatientIndexIdentityVM>>> Handle(PatientSearchByBirthDateQuery request, CancellationToken cancellationToken)
        => (await patientRepository.SearchByBirthDateAsync(request.Filter, cancellationToken))
        .Select(patient => new PatientVM<PatientIndexIdentityVM>
        {
            Name = new PatientIndexIdentityVM
            {
                Id = patient.Id,
                Use = patient.Use,
                Family = patient.Family,
                Given = patient.Given,
            },
            Gender = patient.Gender,
            BirthDate = patient.BirthDate,
            Active = patient.Active,
        });
}