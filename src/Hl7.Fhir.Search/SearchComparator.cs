using System.Runtime.Serialization;

namespace Hl7.Fhir.Search;

public enum SearchComparator
{
    [EnumMember(Value = "eq")] Equal,
    [EnumMember(Value = "ne")] NotEqual,
    [EnumMember(Value = "gt")] GreaterThan,
    [EnumMember(Value = "lt")] LessThan,
    [EnumMember(Value = "ge")] GreaterOrEqual,
    [EnumMember(Value = "le")] LessOrEqual,
    [EnumMember(Value = "sa")] StartsAfter,
    [EnumMember(Value = "eb")] EndsBefore,
    [EnumMember(Value = "ap")] Approximately,
}