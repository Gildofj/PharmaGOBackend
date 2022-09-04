using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Common.Results;

namespace PharmaGOBackend.Application.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;