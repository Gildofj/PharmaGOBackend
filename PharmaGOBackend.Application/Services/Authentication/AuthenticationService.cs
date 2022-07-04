﻿using ErrorOr;
using PharmaGOBackend.Application.Common.Interfaces.Authentication;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Common.Errors;
using PharmaGOBackend.Domain.Entities;
using BC = BCrypt.Net.BCrypt;

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

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            if (_clientRepository.GetClientByEmail(email) is not null)
            {
                return Errors.Client.DuplicateEmail;
            }

            var client = new Client
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = BC.HashPassword(password, BC.GenerateSalt(12)),
            };

            _clientRepository.Add(client);

            var token = _jwtTokenGenerator.GenerateToken(client);

            return new AuthenticationResult(client, token);
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            if (_clientRepository.GetClientByEmail(email) is not Client client || !BC.Verify(password, client.Password))
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(client);

            return new AuthenticationResult(client, token);
        }

    }
}
