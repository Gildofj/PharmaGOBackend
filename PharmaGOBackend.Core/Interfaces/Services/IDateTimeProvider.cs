namespace PharmaGOBackend.Core.Interfaces.Services;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
