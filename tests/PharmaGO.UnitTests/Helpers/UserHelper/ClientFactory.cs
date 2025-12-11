using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Infrastructure.Services;

namespace PharmaGO.UnitTests.Helpers.UserHelper;

public static class ClientFactory
{
    private static readonly IPasswordHashingService PasswordHashing = new PasswordHashingService();

    public static Client GetDefaultClient()
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            FirstName = "Teste",
            LastName = Common.GetRandomName(),
            Email = "teste@teste.com",
            Cpf = "11111111111"
        };

        client.UpdatePassword(PasswordHashing.HashPassword(client, "123"));

        return client;
    }
}