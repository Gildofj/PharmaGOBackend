using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.UnitTests.Helpers.ClientHelper;

namespace PharmaGOBackend.UnitTests.Mocks;

public static class MockClientRepository
{
    public static Mock<IClientRepository> GetClientRepository()
    {
        List<Client> clients = new()
        {
            ClientFactory.GetDefaultClient()
        };

        var mockClientRepository = new Mock<IClientRepository>();

        mockClientRepository.Setup(r => r
                .GetClientByEmailAsync("teste@teste.com")
                );

        mockClientRepository.Setup(r => r
                .AddAsync(It.IsAny<Client>())
                ).ReturnsAsync((Client client) =>
                {
                    clients.Add(client);
                    return client;
                });

        return mockClientRepository;
    }
}