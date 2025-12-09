using PharmaGO.Core.Entities;

namespace PharmaGO.Application.Clients.Common;

public record ClientAuthenticationResult(Client Client, string Token);