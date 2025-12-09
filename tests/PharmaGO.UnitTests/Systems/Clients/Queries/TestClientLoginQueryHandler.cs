using PharmaGO.Application.Clients.Common;
using PharmaGO.Application.Clients.Queries.Login;
using PharmaGO.UnitTests.Helpers.ClientHelper;
using PharmaGO.UnitTests.Helpers.QueriesHelper;
using PharmaGO.Core.Interfaces.Authentication;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Common.Errors;

namespace PharmaGO.UnitTests.Systems.Clients.Queries;

public class TestClientLoginQueryHandler
{
    private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator = new();
    private readonly Mock<IClientRepository> _mockClientRepository = new();

    [Fact]
    public async Task Login_OnSuccess_ReturnsAuthenticationResult()
    {
        _mockClientRepository.Setup(x => x.GetClientByEmailAsync(It.IsAny<string>())
        ).ReturnsAsync(ClientFactory.GetDefaultClient());

        var handler = new ClientLoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            LoginQueryFactory.GetDefault(),
            default
        );

        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<ClientAuthenticationResult>();
    }

    [Fact]
    public async Task Login_PasswordNotInformed_ReturnsInvalidCredentialError()
    {
        _mockClientRepository.Setup(x => x.GetClientByEmailAsync(It.IsAny<string>())
        ).ReturnsAsync(ClientFactory.GetDefaultClient());

        var handler = new ClientLoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            LoginQueryFactory.GetWithoutPassword(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
    }

    [Fact]
    public async Task Login_EmailNotInformed_ReturnsInvalidCredentialError()
    {
        var handler = new ClientLoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            LoginQueryFactory.GetWithoutEmail(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
    }

    [Fact]
    public async Task Login_IncorrectPassword_ReturnsInvalidCredentialError()
    {
        _mockClientRepository.Setup(x => x.GetClientByEmailAsync(It.IsAny<string>())
        ).ReturnsAsync(ClientFactory.GetDefaultClient());

        var handler = new ClientLoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            LoginQueryFactory.GetWithWrongPassword(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
    }
}