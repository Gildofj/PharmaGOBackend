using ErrorOr;
using MediatR;
using PharmaGO.Application.Clients.Common;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Interfaces.Authentication;
using PharmaGO.Core.Interfaces.Persistence;
using BC = BCrypt.Net.BCrypt;

namespace PharmaGO.Application.Clients.Queries.Login;

public class ClientLoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IClientRepository clientRepository)
    : IRequestHandler<ClientLoginQuery, ErrorOr<ClientAuthenticationResult>>
{
    public async Task<ErrorOr<ClientAuthenticationResult>> Handle(ClientLoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (
            await clientRepository.GetClientByEmailAsync(query.Email) is not { } client ||
            !BC.Verify(query.Password, client.Password)
        )
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = jwtTokenGenerator.GenerateToken(client);

        return new ClientAuthenticationResult(client, token);
    }
}