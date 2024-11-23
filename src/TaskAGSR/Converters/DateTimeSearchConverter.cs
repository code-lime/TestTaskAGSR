using Hl7.Fhir.Search;

namespace TaskAGSR.Converters;

public class DateTimeSearchConverter() 
    : TryParseConverter<DateTimeSearch>(DateTimeSearch.TryParse);
