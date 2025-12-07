using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Authentication.Common;

namespace PharmaGOBackend.Application.Authentication.Commands.Register;

public record RegisterClientCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    Guid PharmacyId
) : IRequest<ErrorOr<AuthenticationResult>>;