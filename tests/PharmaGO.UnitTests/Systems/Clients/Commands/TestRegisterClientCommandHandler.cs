using PharmaGO.UnitTests.Helpers.UserHelper;
using PharmaGO.UnitTests.Helpers.CommandsHelper;
using PharmaGO.Application.Clients.Commands.Register;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Infrastructure.Services;

namespace PharmaGO.UnitTests.Systems.Clients.Commands;

public class TestRegisterClientCommandHandler
{
    private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator = new();
    private readonly Mock<IClientRepository> _mockClientRepository = new();
    private readonly IPasswordHashingService _passwordHashing = new PasswordHashingService();

    private RegisterClientCommandHandler _handler;

    public TestRegisterClientCommandHandler()
    {
        _handler = new RegisterClientCommandHandler(
            _mockJwtTokenGenerator.Object,
            _mockClientRepository.Object,
            _passwordHashing
        );
    }

    [Fact]
    public async Task Register_OnSuccess_AddClient()
    {
        var result = await _handler.Handle(
            RegisterClientCommandFactory.GetDefault(),
            default
        );

        _mockClientRepository.Verify(
            x => x.AddAsync(It.Is<Client>(m => m.Id == result.Value.Client.Id)),
            Times.Once
        );
    }

    [Fact]
    public async Task Register_FirstNameNotInformed_ReturnsFirstNameNotInformedError()
    {
        var result = await _handler.Handle(
            RegisterClientCommandFactory.GetWithoutFirstName(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.FirstNameNotInformed);
    }

    [Fact]
    public async Task Register_LastNameNotInformed_ReturnsLastNameNotInformedError()
    {
        var result = await _handler.Handle(
            RegisterClientCommandFactory.GetWithoutLastName(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.LastNameNotInformed);
    }

    [Fact]
    public async Task Register_PasswordNotInformed_ReturnsPasswordNotInformedError()
    {
        var result = await _handler.Handle(
            RegisterClientCommandFactory.GetWithoutPassword(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.PasswordNotInformed);
    }

    [Fact]
    public async Task Register_EmailNotInformed_ReturnsEmailNotInformedError()
    {
        var result = await _handler.Handle(
            RegisterClientCommandFactory.GetWithoutEmail(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.EmailNotInformed);
    }

    [Fact]
    public async Task Register_ExistingEmailInformation_ReturnsDuplicateEmail()
    {
        _mockClientRepository.Setup(x => x.GetClientByEmailAsync(It.IsAny<string>())
        ).ReturnsAsync(ClientFactory.GetDefaultClient());

        var result = await _handler.Handle(
            RegisterClientCommandFactory.GetDefault(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Client.DuplicateEmail);

        _mockClientRepository.Verify(
            x => x.AddAsync(It.IsAny<Client>()),
            Times.Never
        );
    }
}