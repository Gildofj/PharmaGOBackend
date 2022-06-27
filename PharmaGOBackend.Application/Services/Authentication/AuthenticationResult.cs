using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Services.Authentication
{
    public record AuthenticationResult(Client Client, string Token);
}
