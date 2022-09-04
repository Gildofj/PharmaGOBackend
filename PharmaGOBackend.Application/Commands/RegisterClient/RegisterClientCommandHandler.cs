using ErrorOr;
using MediatR;
using PharmaGOBackend.Application.Common.Results;
using PharmaGOBackend.Application.Common.Interfaces.Authentication;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Common.Errors;
using PharmaGOBackend.Domain.Entities;
using BC = BCrypt.Net.BCrypt;

namespace PharmaGOBackend.Application.Commands.RegisterClient;

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

        if (_clientRepository.GetClientByEmail(command.Email) is not null)
        {
            return Errors.Client.DuplicateEmail;
        }

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
}