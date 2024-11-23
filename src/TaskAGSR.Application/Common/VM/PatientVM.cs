using TaskAGSR.Domain.Entities;

namespace TaskAGSR.Application.Common.VM;

public class PatientVM<T>
    where T : PatientIdentityVM
{
    public required T Name { get; set; } = null!;
    public Gender Gender { get; set; } = Gender.Unknown;
    public required DateTime BirthDate { get; set; }
    public bool Active { get; set; } = true;
}
