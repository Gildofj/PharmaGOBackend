using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Common.Authentication;

namespace PharmaGOBackend.Application.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;