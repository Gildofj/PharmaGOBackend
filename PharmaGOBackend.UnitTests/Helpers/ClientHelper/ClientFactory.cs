using PharmaGOBackend.Core.Entities;

namespace PharmaGOBackend.UnitTests.Helpers.ClientHelper;

public static class ClientFactory
{
    public static Client GetDefaultClient()
    {
        return new Client
        {
            Id = Guid.NewGuid(),
            FirstName = "Teste",
            LastName = Common.GetRandomName(),
            Email = "teste@teste.com",
            Password = "123"
        };
    }

    public static Client GetClientWithRepeatedEmail()
    {
        return new Client
        {
            Id = Guid.NewGuid(),
            FirstName = "Teste",
            LastName = Common.GetRandomName(),
            Email = "repeated@teste.com",
            Password = "123"
        };
    }
}