using ErrorOr;
using MediatR;
using PharmaGO.Application.Authentication.Common;
using PharmaGO.Core.Interfaces.Authentication;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using BC = BCrypt.Net.BCrypt;

namespace PharmaGO.Application.Authentication.Queries.Login;

public class LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IClientRepository clientRepository)
    : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
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

        return new AuthenticationResult(client, token);
    }
}