using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Common.Results;

public record AuthenticationResult(Client Client, string Token);