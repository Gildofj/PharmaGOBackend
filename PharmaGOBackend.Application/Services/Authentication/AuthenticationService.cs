using PharmaGOBackend.Application.Common.Interfaces.Authentication;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IClientRepository _clientRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IClientRepository clientRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _clientRepository = clientRepository;
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            if (_clientRepository.GetClientByEmail(email) is not null)
            {
                throw new Exception("Client with given email already exists.");
            }

            var client = new Client
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
            };

            _clientRepository.Add(client);

            var token = _jwtTokenGenerator.GenerateToken(client);

            return new AuthenticationResult(client, token);
        }

        public AuthenticationResult Login(string email, string password)
        {
            if (_clientRepository.GetClientByEmail(email) is not Client client ||  client.Password != password)
            {
                throw new Exception("Invalid e-mail or password.");
            }

            var token = _jwtTokenGenerator.GenerateToken(client);

            return new AuthenticationResult(client, token);
        }

    }
}
