namespace PharmaGOBackend.Core.Services;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
