using PharmaGO.Application.Clients.Common;
using PharmaGO.Application.Clients.Queries.Login;
using PharmaGO.Application.Common.Interfaces;
using PharmaGO.UnitTests.Helpers.UserHelper;
using PharmaGO.UnitTests.Helpers.QueriesHelper;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Infrastructure.Services;

namespace PharmaGO.UnitTests.Systems.Clients.Queries;

public class TestEmployeeLoginQueryHandler
{
    private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator = new();
    private readonly Mock<IClientRepository> _mockClientRepository = new();
    private readonly IPasswordHashingService _passwordHashing = new PasswordHashingService();

    private ClientLoginQueryHandler _handler;

    public TestEmployeeLoginQueryHandler()
    {
        _handler = new ClientLoginQueryHandler(
            _mockJwtTokenGenerator.Object,
            _mockClientRepository.Object,
            _passwordHashing
        );
    }

    [Fact]
    public async Task Login_OnSuccess_ReturnsAuthenticationResult()
    {
        _mockClientRepository.Setup(x => x.GetClientByEmailAsync(It.IsAny<string>())
        ).ReturnsAsync(ClientFactory.GetDefaultClient());

        var result = await _handler.Handle(
            ClientLoginQueryFactory.GetDefault(),
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

        var result = await _handler.Handle(
            ClientLoginQueryFactory.GetWithoutPassword(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
    }

    [Fact]
    public async Task Login_EmailNotInformed_ReturnsInvalidCredentialError()
    {
        var result = await _handler.Handle(
            ClientLoginQueryFactory.GetWithoutEmail(),
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

        var result = await _handler.Handle(
            ClientLoginQueryFactory.GetWithWrongPassword(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
    }
}