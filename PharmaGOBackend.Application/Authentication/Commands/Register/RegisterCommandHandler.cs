using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Authentication.Common;
using PharmaGOBackend.Application.Common.Interfaces.Authentication;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Common.Errors;
using PharmaGOBackend.Domain.Entities;
using BC = BCrypt.Net.BCrypt;

namespace PharmaGOBackend.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IClientRepository _clientRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IClientRepository clientRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _clientRepository = clientRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (ValidateRegisterCredentials(command) is Error error)
        {
            return error;
        };

        var client = new Client
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = BC.HashPassword(command.Password, BC.GenerateSalt(12)),
        };

        _clientRepository.Add(client);

        var token = _jwtTokenGenerator.GenerateToken(client);

        return new AuthenticationResult(client, token);
    }
    
    private Error? ValidateRegisterCredentials(RegisterCommand request)
    {
        if (string.IsNullOrEmpty(request.Email))
        {
            return Errors.Authentication.EmailNotInformed;
        }
            
        if (string.IsNullOrEmpty(request.FirstName))
        {
            return Errors.Authentication.FirstNameNotInformed;
        }
            
        if (string.IsNullOrEmpty(request.LastName))
        {
            return Errors.Authentication.LastNameNotInformed;
        }
            
        if (string.IsNullOrEmpty(request.Password))
        {
            return Errors.Authentication.PasswordNotInformed;
        }
            
        if (_clientRepository.GetClientByEmail(request.Email) is not null)
        {
            return Errors.Client.DuplicateEmail;
        }

        return null;
    }
}