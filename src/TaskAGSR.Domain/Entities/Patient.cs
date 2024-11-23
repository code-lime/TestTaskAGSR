using System.Collections.Immutable;

namespace TaskAGSR.Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public Use Use { get; set; } = Use.Official;
    public required string Family { get; set; }
    public ImmutableArray<string> Given { get; set; } = ImmutableArray<string>.Empty;
    public Gender Gender { get; set; } = Gender.Unknown;
    public required DateTime BirthDate { get; set; }
    public bool Active { get; set; } = true;
}
