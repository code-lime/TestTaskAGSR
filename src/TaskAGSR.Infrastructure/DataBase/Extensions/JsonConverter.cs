using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace TaskAGSR.Infrastructure.DataBase.Extensions;

public class JsonConverter<T>(
    JsonSerializerOptions? options = null,
    ConverterMappingHints? hints = null)
    : ValueConverter<T, string>(
        v => JsonSerializer.Serialize(v, options), 
        v => JsonSerializer.Deserialize<T>(v, options), hints)
    where T : struct;