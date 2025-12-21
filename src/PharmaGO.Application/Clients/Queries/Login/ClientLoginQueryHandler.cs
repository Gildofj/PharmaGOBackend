using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PharmaGO.Application.Clients.Common;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Application.Common.Interfaces;
using PharmaGO.Core.Common.Constants;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Clients.Queries.Login;

public class ClientLoginQueryHandler(
    UserManager<IdentityUser<Guid>> userManager,
    IJwtTokenGenerator jwtTokenGenerator,
    IClientRepository clientRepository
)
    : IRequestHandler<ClientLoginQuery, ErrorOr<ClientAuthenticationResult>>
{
    public async Task<ErrorOr<ClientAuthenticationResult>> Handle(ClientLoginQuery query,
        CancellationToken cancellationToken)
    {
        if (
            await clientRepository.FindByEmailAsync(query.Email) is not { } client ||
            await userManager.FindByEmailAsync(query.Email) is not { } user ||
            !await userManager.CheckPasswordAsync(user, query.Password)
        )
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = await jwtTokenGenerator.GenerateToken(
            new AuthContext
            {
                Person = client,
                User = user,
                UserType = UserType.Client,
            }
        );

        return new ClientAuthenticationResult(client, token);
    }
}