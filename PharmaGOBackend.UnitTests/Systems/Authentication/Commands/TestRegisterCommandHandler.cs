using PharmaGOBackend.Application.Authentication.Commands.Register;
using PharmaGOBackend.Application.Authentication.Common;
using PharmaGOBackend.Application.Common.Interfaces.Authentication;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Common.Errors;
using PharmaGOBackend.UnitTests.Helpers.Authentication.Commands;
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
                RegisterCommandsHelper.GetDefault(), 
                CancellationToken.None
                );

            result.Value.Should().BeOfType<AuthenticationResult>();
        }

        [Fact]
        public async Task Register_FirstNameNotInformed_ReturnsFirstNameNotInformedException()
        {

            var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);
            
            var result = await handler.Handle(
                RegisterCommandsHelper.GetWithoutFirstName(),
                CancellationToken.None
                );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.FirstNameNotInformed);
        }
        
        [Fact]
        public async Task Register_LastNameNotInformed_ReturnsLastNameNotInformedException()
        {

            var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);
            
            var result = await handler.Handle(
                RegisterCommandsHelper.GetWithoutLastName(),
                CancellationToken.None
                );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.LastNameNotInformed);
        }
        
        [Fact]
        public async Task Register_PasswordNotInformed_ReturnsPasswordNotInformedException()
        {

            var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);
            
            var result = await handler.Handle(
                RegisterCommandsHelper.GetWithoutPassword(), 
                CancellationToken.None
                );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.PasswordNotInformed);
        }
        
        [Fact]
        public async Task Register_EmailNotInformed_ReturnsEmailNotInformedException()
        {

            var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);
            
            var result = await handler.Handle(
                RegisterCommandsHelper.GetWithoutEmail(), 
                CancellationToken.None
                );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.EmailNotInformed);
        }
        
        [Fact]
        public async Task Register_ExistingEmailInformation_ReturnsDuplicateEmail()
        {
            
            var handler = new RegisterCommandHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);
            
            var result = await handler.Handle(
                RegisterCommandsHelper.GetWithRepeatedEmail(), 
                CancellationToken.None
            );

            result.Errors.Should().ContainEquivalentOf(Errors.Client.DuplicateEmail);
        }
}