using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Authentication.Common;

namespace PharmaGOBackend.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;