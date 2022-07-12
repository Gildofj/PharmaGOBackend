using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Authentication.Common;

public record AuthenticationResult(Client Client, string Token);