using ErrorOr;
using PharmaGOBackend.Application.Commands.RegisterClient;
using PharmaGOBackend.Application.Common.Results;
using PharmaGOBackend.Core.Authentication;
using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.UnitTests.Helpers.Authentication.CommandsHelper;
using PharmaGOBackend.UnitTests.Mocks;

namespace PharmaGOBackend.UnitTests.Systems.Authentication.Commands;

public class TestRegisterCommandHandler
{
    private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
    private readonly Mock<IClientRepository> _mockClientRepository;

    public TestRegisterCommandHandler()
    {
        _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
        _mockClientRepository = MockClientRepository.GetClientRepository();
    }

    [Fact]
    public async Task Register_OnSuccess_ReturnsAuthenticationResult()
    {
        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            RegisterCommandsFactory.GetDefault(),
            CancellationToken.None
            );

        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<AuthenticationResult>();
    }

    [Fact]
    public async Task Register_FirstNameNotInformed_ReturnsFirstNameNotInformedException()
    {

        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            RegisterCommandsFactory.GetWithoutFirstName(),
            CancellationToken.None
            );

        result.IsError.Should().BeTrue();
        Assert.Collection(
            result.Errors,
            item => Assert.Equal("Auth.FirstNameNotInformed", item.Code)
            );
    }

    [Fact]
    public async Task Register_LastNameNotInformed_ReturnsLastNameNotInformedException()
    {

        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            RegisterCommandsFactory.GetWithoutLastName(),
            CancellationToken.None
            );

        result.IsError.Should().BeTrue();
        Assert.Collection(result.Errors, item => Assert.Equal("Auth.LastNameNotInformed", item.Code));
    }

    [Fact]
    public async Task Register_PasswordNotInformed_ReturnsPasswordNotInformedException()
    {

        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            RegisterCommandsFactory.GetWithoutPassword(),
            CancellationToken.None
            );

        result.IsError.Should().BeTrue();
        Assert.Collection(result.Errors, item => Assert.Equal("Auth.PasswordNotInformed", item.Code));
    }

    [Fact]
    public async Task Register_EmailNotInformed_ReturnsEmailNotInformedException()
    {

        var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        var result = await handler.Handle(
            RegisterCommandsFactory.GetWithoutEmail(),
            CancellationToken.None
            );

        result.IsError.Should().BeTrue();
        Assert.Collection(result.Errors, item => Assert.Equal("Auth.EmailNotInformed", item.Code));
    }

    // TODO: Mock client with repeated email don't return in mock get
    //[Fact]
    //public async Task Register_ExistingEmailInformation_ReturnsDuplicateEmail()
    //{

    //    var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

    //    var result = await handler.Handle(
    //        RegisterCommandsFactory.GetWithRepeatedEmail(),
    //        CancellationToken.None
    //    );

    //    result.IsError.Should().BeTrue();
    //    result.Errors.Should().ContainEquivalentOf(Errors.Client.DuplicateEmail);
    //}
}