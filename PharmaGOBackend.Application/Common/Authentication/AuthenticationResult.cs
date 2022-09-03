using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Common.Authentication;

public record AuthenticationResult(Client Client, string Token);