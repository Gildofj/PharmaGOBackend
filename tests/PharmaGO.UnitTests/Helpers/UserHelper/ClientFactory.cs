using PharmaGO.Core.Entities;

namespace PharmaGO.UnitTests.Helpers.UserHelper;

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
            Password = BC.HashPassword("123", BC.GenerateSalt(12))
        };
    }
}