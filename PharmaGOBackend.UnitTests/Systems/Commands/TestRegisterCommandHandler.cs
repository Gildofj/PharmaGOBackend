using PharmaGOBackend.Application.Commands.RegisterClient;
using PharmaGOBackend.Core.Authentication;
using PharmaGOBackend.Core.Common.Errors;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.UnitTests.Helpers.Authentication.CommandsHelper;
using PharmaGOBackend.UnitTests.Helpers.ClientHelper;

namespace PharmaGOBackend.UnitTests.Systems.Authentication.Commands;

public class TestRegisterCommandHandler
{
    private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
    private readonly Mock<IClientRepository> _mockClientRepository;

    public TestRegisterCommandHandler()
    {
        _mockJwtTokenGenerator = new();
        _mockClientRepository = new();
    }

    [Fact]
    public async Task Register_OnSuccess_ReturnsAuthenticationResult()
    {
        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            RegisterCommandsFactory.GetDefault(),
            default
            );

        _mockClientRepository.Verify(
            x => x.AddAsync(It.Is<Client>(m => m.Id == result.Value.Client.Id)),
            Times.Once
            );
    }

    [Fact]
    public async Task Register_FirstNameNotInformed_ReturnsFirstNameNotInformedException()
    {
        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            RegisterCommandsFactory.GetWithoutFirstName(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.FirstNameNotInformed);
    }

    [Fact]
    public async Task Register_LastNameNotInformed_ReturnsLastNameNotInformedException()
    {
        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            RegisterCommandsFactory.GetWithoutLastName(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.LastNameNotInformed);
    }

    [Fact]
    public async Task Register_PasswordNotInformed_ReturnsPasswordNotInformedException()
    {
        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            RegisterCommandsFactory.GetWithoutPassword(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.PasswordNotInformed);
    }

    [Fact]
    public async Task Register_EmailNotInformed_ReturnsEmailNotInformedException()
    {
        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            RegisterCommandsFactory.GetWithoutEmail(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.EmailNotInformed);
    }

    [Fact]
    public async Task Register_ExistingEmailInformation_ReturnsDuplicateEmail()
    {
        _mockClientRepository.Setup(
             x => x.GetClientByEmailAsync(It.IsAny<string>())
             ).ReturnsAsync(ClientFactory.GetDefaultClient());

        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
          RegisterCommandsFactory.GetDefault(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Client.DuplicateEmail);
    }
}