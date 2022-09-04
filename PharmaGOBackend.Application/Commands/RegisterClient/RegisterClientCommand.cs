using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Common.Results;

namespace PharmaGOBackend.Application.Commands.RegisterClient;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;