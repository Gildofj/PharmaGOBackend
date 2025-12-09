using ErrorOr;
using MediatR;
using PharmaGO.Application.Authentication.Common;

namespace PharmaGO.Application.Authentication.Commands.Register;

public record RegisterClientCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;