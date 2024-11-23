using System.Collections.Immutable;
using TaskAGSR.Domain.Entities;

namespace TaskAGSR.Application.Common.VM;

public class PatientIdentityVM
{
    public Use Use { get; set; } = Use.Official;
    public required string Family { get; set; }
    public ImmutableArray<string> Given { get; set; } = ImmutableArray<string>.Empty;
}
