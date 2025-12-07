using ErrorOr;
using MediatR;
using PharmaGO.Application.Authentication.Common;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using BC = BCrypt.Net.BCrypt;
using PharmaGO.Core.Interfaces.Authentication;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Authentication.Commands.Register;

public class RegisterClientCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IClientRepository clientRepository)
    : IRequestHandler<RegisterClientCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterClientCommand command,
        CancellationToken cancellationToken
    )
    {
        await Task.CompletedTask;

        if (await ValidateRegisterCredentials(command) is { } error)
            return error;

        var client = new Client
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = BC.HashPassword(command.Password, BC.GenerateSalt(12)),
            PharmacyId = command.PharmacyId,
        };

        await clientRepository.AddAsync(client);

        var token = jwtTokenGenerator.GenerateToken(client);

        return new AuthenticationResult(client, token);
    }

    private async Task<Error?> ValidateRegisterCredentials(RegisterClientCommand command)
    {
        if (string.IsNullOrEmpty(command.Email))
        {
            return Errors.Authentication.EmailNotInformed;
        }

        if (string.IsNullOrEmpty(command.FirstName))
        {
            return Errors.Authentication.FirstNameNotInformed;
        }

        if (string.IsNullOrEmpty(command.LastName))
        {
            return Errors.Authentication.LastNameNotInformed;
        }

        if (string.IsNullOrEmpty(command.Password))
        {
            return Errors.Authentication.PasswordNotInformed;
        }

        if (await clientRepository.GetClientByEmailAsync(command.Email) is not null)
        {
            return Errors.Client.DuplicateEmail;
        }

        return null;
    }
}