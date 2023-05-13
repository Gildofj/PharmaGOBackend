using PharmaGOBackend.Core.Authentication;
using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.Core.Common.Errors;
using PharmaGOBackend.Application.Queries.Login;
using PharmaGOBackend.UnitTests.Helpers.Authentication.QueriesHelper;
using PharmaGOBackend.UnitTests.Helpers.ClientHelper;
using PharmaGOBackend.Application.Common.Results;

namespace PharmaGOBackend.UnitTests.Systems.Queries;

public class TestLoginQueryHandler
{
    private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
    private readonly Mock<IClientRepository> _mockClientRepository;

    public TestLoginQueryHandler()
    {
        _mockJwtTokenGenerator = new();
        _mockClientRepository = new();
    }

    [Fact]
    public async Task Login_OnSuccess_ReturnsAuthenticationResult()
    {
        _mockClientRepository.Setup(
            x => x.GetClientByEmailAsync(It.IsAny<string>())
            ).ReturnsAsync(ClientFactory.GetDefaultClient());

        var handler = new LoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            LoginQueryFactory.GetDefault(),
            default
            );

        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<AuthenticationResult>();
    }

    [Fact]
    public async Task Login_PasswordNotInformed_ReturnsInvalidCredentialError()
    {
        _mockClientRepository.Setup(
            x => x.GetClientByEmailAsync(It.IsAny<string>())
            ).ReturnsAsync(ClientFactory.GetDefaultClient());

        var handler = new LoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

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
        var handler = new LoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

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
        _mockClientRepository.Setup(
            x => x.GetClientByEmailAsync(It.IsAny<string>())
            ).ReturnsAsync(ClientFactory.GetDefaultClient());

        var handler = new LoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            LoginQueryFactory.GetWithWrongPassword(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
    }
}
