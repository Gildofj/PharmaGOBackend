using ErrorOr;
using MediatR;
using PharmaGO.Application.Clients.Common;

namespace PharmaGO.Application.Clients.Queries.Login;

public record ClientLoginQuery(string Email, string Password) : IRequest<ErrorOr<ClientAuthenticationResult>>;