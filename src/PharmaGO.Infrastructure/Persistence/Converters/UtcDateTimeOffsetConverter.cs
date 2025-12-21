using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PharmaGO.Infrastructure.Persistence.Converters;

public class UtcDateTimeOffsetConverter() : ValueConverter<DateTimeOffset, DateTimeOffset>(
    d => d.ToUniversalTime(),
    d => d.ToUniversalTime()
);