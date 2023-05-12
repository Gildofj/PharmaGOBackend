using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.UnitTests.Helpers.ClientHelper;

namespace PharmaGOBackend.UnitTests.Mocks;

public static class MockClientRepository
{
    public static Mock<IClientRepository> GetClientRepository()
    {
        List<Client> clients = new List<Client>
        {
            ClientFactory.GetDefaultClient(),
            ClientFactory.GetClientWithRepeatedEmail()
        };

        var mockClientRepository = new Mock<IClientRepository>();

        var repeatedEmailClient = clients.FirstOrDefault(c => c.Email.Contains("repeated"))!.Email;

        mockClientRepository.Setup(r =>
            r.GetClientByEmail(repeatedEmailClient)
        );

        mockClientRepository.Setup(r => r
                .AddAsync(It.IsAny<Client>()))
                .Returns((Client client) =>
                {
                    clients.Add(client);
                    return client;
                });

        return mockClientRepository;
    }
}