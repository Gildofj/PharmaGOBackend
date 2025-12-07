using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Authentication.Common;

namespace PharmaGOBackend.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;