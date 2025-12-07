using PharmaGO.Core.Entities;

namespace PharmaGO.Application.Authentication.Common;

public record AuthenticationResult(Client Client, string Token);