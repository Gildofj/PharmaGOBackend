using ErrorOr;
using MediatR;
using PharmaGO.Application.Authentication.Common;

namespace PharmaGO.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;