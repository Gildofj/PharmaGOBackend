using ErrorOr;
using MediatR;
using PharmaGO.Application.Clients.Common;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Clients.Queries.Login;

public class ClientLoginQueryHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IClientRepository clientRepository,
    IPasswordHashingService passwordHashing)
    : IRequestHandler<ClientLoginQuery, ErrorOr<ClientAuthenticationResult>>
{
    public async Task<ErrorOr<ClientAuthenticationResult>> Handle(ClientLoginQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (
            await clientRepository.GetClientByEmailAsync(query.Email) is not { } client ||
            !passwordHashing.VerifyPasswordHash(client, query.Password, client.Password)
        )
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = jwtTokenGenerator.GenerateToken(client);

        return new ClientAuthenticationResult(client, token);
    }
}