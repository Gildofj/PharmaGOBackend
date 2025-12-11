using ErrorOr;
using MediatR;
using PharmaGO.Application.Clients.Common;

namespace PharmaGO.Application.Clients.Commands.Register;

public record RegisterClientCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone, 
    string Cpf
) : IRequest<ErrorOr<ClientAuthenticationResult>>;