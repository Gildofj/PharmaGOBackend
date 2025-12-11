using ErrorOr;
using MediatR;
using PharmaGO.Application.Clients.Common;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Clients.Commands.Register;

public class RegisterClientCommandHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IClientRepository clientRepository,
    IPasswordHashingService passwordHashing)
    : IRequestHandler<RegisterClientCommand, ErrorOr<ClientAuthenticationResult>>
{
    public async Task<ErrorOr<ClientAuthenticationResult>> Handle(
        RegisterClientCommand command,
        CancellationToken cancellationToken
    )
    {
        if (await clientRepository.GetClientByEmailAsync(command.Email) is not null)
        {
            return Errors.Client.DuplicateEmail;
        }
        
        if (string.IsNullOrEmpty(command.Password))
        {
            return Errors.Authentication.PasswordNotInformed;
        }

        var clientResult = Client.CreateClient(
            firstName: command.FirstName,
            lastName: command.LastName,
            email: command.Email,
            phone: command.Phone,
            cpf: command.Cpf
        );

        if (clientResult.IsError)
        {
            return clientResult.Errors;
        }

        var client = clientResult.Value;

        client.UpdatePassword(passwordHashing.HashPassword(client, command.Password));

        await clientRepository.AddAsync(client);

        var token = jwtTokenGenerator.GenerateToken(client);

        return new ClientAuthenticationResult(client, token);
    }
}