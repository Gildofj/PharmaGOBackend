using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PharmaGO.Application.Clients.Common;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Application.Common.Interfaces;
using PharmaGO.Core.Common.Constants;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Clients.Commands.Register;

public class RegisterClientCommandHandler(
    UserManager<IdentityUser<Guid>> userManager,
    IJwtTokenGenerator jwtTokenGenerator,
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork,
    ILogger<RegisterClientCommandHandler> logger
) : IRequestHandler<RegisterClientCommand, ErrorOr<ClientAuthenticationResult>>
{
    public async Task<ErrorOr<ClientAuthenticationResult>> Handle(
        RegisterClientCommand command,
        CancellationToken cancellationToken
    )
    {
        //TODO: Usuário de cliente e de employee podem ter acessos diferentes
        if (await userManager.FindByEmailAsync(command.Email) is not null)
        {
            logger.LogWarning("Registration attempt with duplicate email: {Email}", command.Email);
            return Errors.Client.DuplicateEmail;
        }

        if (await clientRepository.ExistsByCpfAsync(command.Cpf))
        {
            logger.LogWarning("Registration attempt with duplicate CPF: {Cpf}", command.Cpf);
            return Errors.Client.DuplicateCpf;
        }

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
        IdentityUser<Guid>? identityUser = null;

        try
        {
            var identityUserResult = await CreateClientIdentityUser(command, client.Id);

            if (identityUserResult.IsError)
            {
                return identityUserResult.Errors;
            }

            identityUser = identityUserResult.Value;

            await clientRepository.AddAsync(client);

            var token = await jwtTokenGenerator.GenerateToken(
                new AuthContext
                {
                    Person = client,
                    User = identityUser,
                    UserType = UserType.Client,
                }
            );

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation(
                "Client {ClientId} registered successfully!",
                client.Id
            );

            return new ClientAuthenticationResult(client, token);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Unexpected error occured while registering client with email {Email}",
                command.Email
            );

            if (identityUser != null)
            {
                try
                {
                    await userManager.DeleteAsync(identityUser);
                    logger.LogInformation(
                        "Rolled back identity user {UserId} after registration failure",
                        identityUser.Id
                    );
                }
                catch (Exception deleteEx)
                {
                    logger.LogError(deleteEx, "Failed to rollback identity user {UserId}", identityUser.Id);
                }
            }

            return Error.Failure("Client.RegistrationFailed", "Registration failed");
        }
    }

    private async Task<ErrorOr<IdentityUser<Guid>>> CreateClientIdentityUser(RegisterClientCommand command,
        Guid clientId)
    {
        var identityUser = new IdentityUser<Guid>
        {
            Id = clientId,
            UserName = command.Email,
            Email = command.Email,
            PhoneNumber = command.Phone,
            EmailConfirmed = true
        };

        var createUserResult = await userManager.CreateAsync(identityUser, command.Password);
        if (!createUserResult.Succeeded)
        {
            logger.LogError(
                "Failed to create user to client {ClientId} for email {Email}. Errors: {Errors}",
                identityUser.Id,
                command.Email,
                string.Join(", ", createUserResult.Errors.Select(e => e.Description))
            );

            return createUserResult.Errors
                .Select(e => Error.Validation(e.Code, e.Description))
                .ToList();
        }

        var addToRoleResult = await userManager.AddToRoleAsync(identityUser, nameof(UserType.Client));
        if (!addToRoleResult.Succeeded)
        {
            logger.LogError(
                "Failed to add to role {RoleName} for client {ClientId}. Errors: {Errors}",
                nameof(UserType.Client),
                identityUser.Id,
                string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))
            );

            await userManager.DeleteAsync(identityUser);
            return addToRoleResult.Errors
                .Select(e => Error.Validation(e.Code, e.Description))
                .ToList();
        }

        return identityUser;
    }
}