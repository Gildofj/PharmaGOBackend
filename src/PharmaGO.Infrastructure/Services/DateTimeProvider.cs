using PharmaGO.Core.Interfaces.Services;

namespace PharmaGO.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
