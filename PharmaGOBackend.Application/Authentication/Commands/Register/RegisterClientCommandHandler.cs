using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Common.Errors;
using PharmaGOBackend.Core.Entities;
using BC = BCrypt.Net.BCrypt;
using PharmaGOBackend.Application.Authentication.Common;
using PharmaGOBackend.Core.Interfaces.Authentication;
using PharmaGOBackend.Core.Interfaces.Persistence;

namespace PharmaGOBackend.Application.Authentication.Commands.Register;

public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IClientRepository _clientRepository;

    public RegisterClientCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IClientRepository clientRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _clientRepository = clientRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterClientCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (await ValidateRegisterCredentials(command) is Error error)
            return error;

        var client = new Client
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = BC.HashPassword(command.Password, BC.GenerateSalt(12)),
            PharmacyId = command.PharmacyId,
        };

        await _clientRepository.AddAsync(client);

        var token = _jwtTokenGenerator.GenerateToken(client);

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

        if (await _clientRepository.GetClientByEmailAsync(command.Email) is not null)
        {
            return Errors.Client.DuplicateEmail;
        }

        return null;
    }
}