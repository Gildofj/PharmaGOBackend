using PharmaGOBackend.Application.Common.Interfaces.Authentication;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Application.Services.Authentication;
using PharmaGOBackend.Domain.Common.Errors;

namespace PharmaGOBackend.UnitTests.Systems.Services
{
    public class TestAuthenticationService
    {
        [Fact]
        public void Register_OnSuccess_ReturnsAuthenticationResult()
        {
            var mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            var mockClientRepository = new Mock<IClientRepository>();

            var sut = new AuthenticationService(mockJwtTokenGenerator.Object, mockClientRepository.Object);

            var result = sut.Register(
                "Cliente", 
                "Teste", 
                "teste@teste.com", 
                "123"
                );

            result.Value.Should().BeOfType<AuthenticationResult>();
        }

        [Fact]
        public void Register_FirstNameNotInformed_ReturnsFirstNameNotInformedException()
        {
            var mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            var mockClientRepository = new Mock<IClientRepository>();

            var sut = new AuthenticationService(mockJwtTokenGenerator.Object, mockClientRepository.Object);

            var result = sut.Register(
                "",
                "Teste",
                "teste@teste.com",
                "123"
            );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.FirstNameNotInformed);
        }
        
        [Fact]
        public void Register_LastNameNotInformed_ReturnsLastNameNotInformedException()
        {
            var mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            var mockClientRepository = new Mock<IClientRepository>();

            var sut = new AuthenticationService(mockJwtTokenGenerator.Object, mockClientRepository.Object);

            var result = sut.Register(
                "Cliente",
                "",
                "teste@teste.com",
                "123"
            );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.LastNameNotInformed);
        }
        
        [Fact]
        public void Register_PasswordNotInformed_ReturnsPasswordNotInformedException()
        {
            var mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            var mockClientRepository = new Mock<IClientRepository>();

            var sut = new AuthenticationService(mockJwtTokenGenerator.Object, mockClientRepository.Object);

            var result = sut.Register(
                "Cliente",
                "Teste",
                "teste@teste.com",
                ""
            );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.PasswordNotInformed);
        }
        
        [Fact]
        public void Register_EmailNotInformed_ReturnsEmailNotInformedException()
        {
            var mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            var mockClientRepository = new Mock<IClientRepository>();

            var sut = new AuthenticationService(mockJwtTokenGenerator.Object, mockClientRepository.Object);

            var result = sut.Register(
                "Cliente",
                "Teste",
                "",
                "123"
            );

            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.EmailNotInformed);
        }

        //TODO: Study how to use mock for memory database in tests
        [Fact]
        public void Register_ExistingEmailInformation_ReturnsDuplicateEmail()
        {
            var mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            var mockClientRepository = new Mock<IClientRepository>();

            var sut = new AuthenticationService(mockJwtTokenGenerator.Object, mockClientRepository.Object);
            
            var result = sut.Register(
                "Cliente",
                "Teste",
                "teste@teste.com",
                "123"
            );

            result.Errors.Should().ContainEquivalentOf(Errors.Client.DuplicateEmail);
        }
    }
}
