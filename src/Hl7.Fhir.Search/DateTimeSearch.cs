using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Hl7.Fhir.Search;

public class DateTimeSearch : ISearch<DateTime>
{
    private const double ApproximateMultiplier = .1;

    private delegate bool TryParseDateTimeSearch(string input, [NotNullWhen(true)] out DateTimeSearch? search);

    private static readonly TryParseDateTimeSearch[] parsers = EnumerateParsers()
        .ToArray();
    private static readonly ImmutableDictionary<string, SearchComparator> comparators = Enum.GetValues<SearchComparator>()
        .ToImmutableDictionary(v => v.GetType()
            .GetField(v.ToString(), BindingFlags.Public | BindingFlags.Static)!
            .GetCustomAttribute<EnumMemberAttribute>()!
            .Value!, v => v);

    public SearchComparator Comparator { get; }
    public DateTimeOffset Begin { get; }
    public DateTimeOffset End { get; }

    public Expression<Func<DateTime, bool>> SearchExpression { get; }

    public DateTimeSearch(
        SearchComparator comparator,
        int year, int? month = null, int? day = null,
        int? hour = null, int? minute = null, int? second = null,
        TimeSpan? utcOffset = null)
    {
        Comparator = comparator;
        Begin = new DateTimeOffset(year, month ?? 1, day ?? 1, hour ?? 0, minute ?? 0, second ?? 0, utcOffset ?? TimeSpan.Zero);
        End = new DateTimeOffset(year, month ?? 12, day ?? DateTime.DaysInMonth(year, month ?? 12), hour ?? 23, minute ?? 59, second ?? 59, utcOffset ?? TimeSpan.Zero);

        SearchExpression = BuildSearch();
    }

    private Expression<Func<DateTime, bool>> BuildSearch()
    {
        switch (Comparator)
        {
            case SearchComparator.Equal: return v => Begin <= v && v <= End;
            case SearchComparator.NotEqual: return v => Begin > v || v > End;
            case SearchComparator.LessThan: return v => v < Begin;
            case SearchComparator.GreaterThan: return v => v > End;
            case SearchComparator.LessOrEqual: return v => v <= End;
            case SearchComparator.GreaterOrEqual: return v => v >= End;
            case SearchComparator.StartsAfter: return v => v > End;
            case SearchComparator.EndsBefore: return v => v < Begin;
            case SearchComparator.Approximately:
                long intervalTicks = End.Ticks - Begin.Ticks;
                long differenceTicks = (long)(intervalTicks * ApproximateMultiplier);

                DateTimeOffset approximateStart = Begin.AddTicks(-differenceTicks);
                DateTimeOffset approximateEnd = End.AddTicks(differenceTicks);

                return v => approximateStart <= v && v <= approximateEnd;
            default:
                throw new NotSupportedException($"{nameof(SearchComparator)}.{Comparator} not support");
        }
    }

    public static bool TryParse([NotNullWhen(true)] string? input, [NotNullWhen(true)] out DateTimeSearch? search)
    {
        if (input is null)
        {
            search = null;
            return false;
        }

        foreach (TryParseDateTimeSearch parser in parsers)
            if (parser(input, out search))
                return true;

        search = null;
        return false;
    }
    private static IEnumerable<TryParseDateTimeSearch> EnumerateParsers()
    {
        static TryParseDateTimeSearch CreateTryParse(string format, int partIndex)
        {
            IFormatProvider provider = CultureInfo.InvariantCulture.DateTimeFormat;
            return (string input, [NotNullWhen(true)] out DateTimeSearch? data) =>
            {
                foreach ((string prefix, SearchComparator comparator) in comparators)
                {
                    if (!input.StartsWith(prefix))
                        continue;
                    if (!DateTimeOffset.TryParseExact(input[prefix.Length..], format, provider, DateTimeStyles.AssumeUniversal, out DateTimeOffset offset))
                        continue;
                    int year = offset.Year;
                    int? month = partIndex > 0 ? offset.Month : null;
                    int? day = partIndex > 1 ? offset.Day : null;
                    int? hour = partIndex > 2 ? offset.Hour : null;
                    int? minute = partIndex > 3 ? offset.Minute : null;
                    int? second = partIndex > 4 ? offset.Second : null;
                    data = new DateTimeSearch(comparator, year, month, day, hour, minute, second, offset.Offset);
                    return true;
                }
                data = null;
                return false;
            };
        }

        string[] timeParts = ["yyyy", "-MM", "-dd", "THH", ":mm", ":ss"];
        string[] timeZones = ["Z", "z", "zz", "zzz"];

        string format = "";
        int partIndex = 0;
        foreach (string part in timeParts)
        {
            format += part;
            yield return CreateTryParse(format, partIndex);
            foreach (string timeZone in timeZones)
                yield return CreateTryParse(format + timeZone, partIndex);

            partIndex++;
        }
    }
}