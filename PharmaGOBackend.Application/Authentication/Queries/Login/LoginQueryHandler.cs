using ErrorOr;
using MediatR;
using PharmaGOBackend.Core.Interfaces.Authentication;
using PharmaGOBackend.Core.Interfaces.Persistence;
using PharmaGOBackend.Core.Common.Errors;
using PharmaGOBackend.Core.Entities;
using BC = BCrypt.Net.BCrypt;
using PharmaGOBackend.Application.Authentication.Common;

namespace PharmaGOBackend.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IClientRepository _clientRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IClientRepository clientRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _clientRepository = clientRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (
            await _clientRepository.GetClientByEmailAsync(query.Email) is not Client client ||
            !BC.Verify(query.Password, client.Password)
        )
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(client);

        return new AuthenticationResult(client, token);
    }
}