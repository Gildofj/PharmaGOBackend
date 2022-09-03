using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Common.Authentication;

namespace PharmaGOBackend.Application.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;