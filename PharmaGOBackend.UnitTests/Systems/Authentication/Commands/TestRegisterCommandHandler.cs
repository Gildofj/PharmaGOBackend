using PharmaGOBackend.Application.Authentication.Commands.Register;
using PharmaGOBackend.Application.Authentication.Common;
using PharmaGOBackend.Application.Common.Interfaces.Authentication;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Common.Errors;
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

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.FirstNameNotInformed);
        }
        
        [Fact]
        public async Task Register_LastNameNotInformed_ReturnsLastNameNotInformedException()
        {

            var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);
            
            var result = await handler.Handle(
                RegisterCommandsFactory.GetWithoutLastName(),
                CancellationToken.None
                );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.LastNameNotInformed);
        }
        
        [Fact]
        public async Task Register_PasswordNotInformed_ReturnsPasswordNotInformedException()
        {

            var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);
            
            var result = await handler.Handle(
                RegisterCommandsFactory.GetWithoutPassword(), 
                CancellationToken.None
                );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.PasswordNotInformed);
        }
        
        [Fact]
        public async Task Register_EmailNotInformed_ReturnsEmailNotInformedException()
        {

            var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);
            
            var result = await handler.Handle(
                RegisterCommandsFactory.GetWithoutEmail(), 
                CancellationToken.None
                );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.EmailNotInformed);
        }
        
        // TODO: Mock client with repeated email don't return in mock get
        [Fact]
        public async Task Register_ExistingEmailInformation_ReturnsDuplicateEmail()
        {
            
            var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);
            
            var result = await handler.Handle(
                RegisterCommandsFactory.GetWithRepeatedEmail(), 
                CancellationToken.None
            );

            result.Errors.Should().ContainEquivalentOf(Errors.Client.DuplicateEmail);
        }
}