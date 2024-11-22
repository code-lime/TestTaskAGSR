using MediatR;
using TaskAGSR.Application.Common.Interfaces;
using TaskAGSR.Application.Common.VM;

namespace TaskAGSR.Application.Data.Queries;

public record PatientCreateQuery(PatientVM<PatientIdentityVM> Patient) : IRequest<Guid>;
public record PatientGetQuery(Guid Id) : IRequest<PatientVM<PatientIndexIdentityVM>?>;
public record PatientUpdateQuery(Guid Id, PatientVM<PatientIdentityVM> Patient) : IRequest<bool>;
public record PatientDeleteQuery(Guid Id) : IRequest<bool>;

public class PatientQueryHandler(
    IPatientRepository patientRepository) :
    IRequestHandler<PatientCreateQuery, Guid>,
    IRequestHandler<PatientGetQuery, PatientVM<PatientIndexIdentityVM>?>,
    IRequestHandler<PatientUpdateQuery, bool>,
    IRequestHandler<PatientDeleteQuery, bool>
{
    public async Task<Guid> Handle(PatientCreateQuery request, CancellationToken cancellationToken)
        => await patientRepository.CreateAsync(new Domain.Entities.Patient
        {
            Use = request.Patient.Name.Use,
            Family = request.Patient.Name.Family,
            Given = request.Patient.Name.Given,
            Gender = request.Patient.Gender,
            BirthDate = request.Patient.BirthDate,
            Active = request.Patient.Active,
        }, cancellationToken);

    public async Task<PatientVM<PatientIndexIdentityVM>?> Handle(PatientGetQuery request, CancellationToken cancellationToken)
        => await patientRepository.ReadAsync(request.Id, cancellationToken) is Domain.Entities.Patient patient
        ? new PatientVM<PatientIndexIdentityVM>
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
        }
        : null;

    public async Task<bool> Handle(PatientUpdateQuery request, CancellationToken cancellationToken)
        => await patientRepository.UpdateAsync(request.Id, new Domain.Entities.Patient
        {
            Use = request.Patient.Name.Use,
            Family = request.Patient.Name.Family,
            Given = request.Patient.Name.Given,
            Gender = request.Patient.Gender,
            BirthDate = request.Patient.BirthDate,
            Active = request.Patient.Active,
        }, cancellationToken);

    public async Task<bool> Handle(PatientDeleteQuery request, CancellationToken cancellationToken)
        => await patientRepository.DeleteAsync(request.Id, cancellationToken);
}