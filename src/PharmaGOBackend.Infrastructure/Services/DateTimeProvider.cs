using PharmaGOBackend.Core.Interfaces.Services;

namespace PharmaGOBackend.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
