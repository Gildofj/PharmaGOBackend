using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Entities;
using PharmaGOBackend.UnitTests.Helpers.MockClient;

namespace PharmaGOBackend.UnitTests.Mocks;

public static class MockClientRepository
{
    public static Mock<IClientRepository> GetClientRepository()
    {
        List<Client> clients = new List<Client>
        {
            ClientHelper.GetDefaultClient(),
            ClientHelper.GetClientWithRepeatedEmail()
        };

        var mockClientRepository = new Mock<IClientRepository>();

        mockClientRepository.Setup(r =>
            r.GetClientByEmail(
                clients.FirstOrDefault(c => c.Email.Contains("repeated"))!.Email
            )
        );

        mockClientRepository.Setup(r => r
                .Add(It.IsAny<Client>()))
                .Returns((Client client) =>
                {
                    clients.Add(client);
                    return client;
                });

        return mockClientRepository;
    }
}