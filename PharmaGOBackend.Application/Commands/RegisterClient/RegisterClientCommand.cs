using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Common.Results;

namespace PharmaGOBackend.Application.Commands.RegisterClient;

public record RegisterClientCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    Guid PharmacyId
    ) : IRequest<ErrorOr<AuthenticationResult>>;