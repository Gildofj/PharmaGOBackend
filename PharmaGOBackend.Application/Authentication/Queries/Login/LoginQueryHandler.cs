using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Interfaces.Authentication;
using PharmaGOBackend.Core.Interfaces.Persistence;
using PharmaGOBackend.Core.Common.Errors;
using PharmaGOBackend.Core.Entities;
using BC = BCrypt.Net.BCrypt;
using PharmaGOBackend.Application.Authentication.Common;

namespace PharmaGOBackend.Application.Authentication.Queries.Login;

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