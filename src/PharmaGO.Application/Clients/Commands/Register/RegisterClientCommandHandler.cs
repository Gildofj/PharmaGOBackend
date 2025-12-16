using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PharmaGO.Application.Clients.Common;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Application.Common.Interfaces;
using PharmaGO.Core.Common.Constants;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Clients.Commands.Register;

public class RegisterClientCommandHandler(
    UserManager<IdentityUser<Guid>> userManager,
    IJwtTokenGenerator jwtTokenGenerator,
    IClientRepository clientRepository)
    : IRequestHandler<RegisterClientCommand, ErrorOr<ClientAuthenticationResult>>
{
    public async Task<ErrorOr<ClientAuthenticationResult>> Handle(
        RegisterClientCommand command,
        CancellationToken cancellationToken
    )
    {
        if (await userManager.FindByEmailAsync(command.Email) is not null)
        {
            return Errors.Client.DuplicateEmail;
        }
        
        var identityUser = new IdentityUser<Guid>
        {
            UserName = command.Email,
            Email = command.Email,
            PhoneNumber = command.Phone,
            EmailConfirmed = true
        };

        var createUserResult = await userManager.CreateAsync(identityUser, command.Password);

        if (!createUserResult.Succeeded)
        {
            return createUserResult.Errors
                .Select(e => Error.Validation(e.Code, e.Description))
                .ToList();
        }
        
        await userManager.AddToRoleAsync(identityUser, nameof(UserType.Client));

        var clientResult = Client.Create(
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

        await clientRepository.AddAsync(client);

        var token = await jwtTokenGenerator.GenerateToken(
            new AuthContext
            {
                Person = client,
                User = identityUser,
                UserType = UserType.Client,
            }
        );

        return new ClientAuthenticationResult(client, token);
    }
}