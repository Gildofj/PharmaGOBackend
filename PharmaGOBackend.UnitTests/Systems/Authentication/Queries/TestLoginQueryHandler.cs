using PharmaGOBackend.Application.Authentication.Common;
using PharmaGOBackend.Application.Common.Interfaces.Authentication;
using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.UnitTests.Mocks;
using PharmaGOBackend.Domain.Common.Errors;
using PharmaGOBackend.Application.Authentication.Queries.Login;
using PharmaGOBackend.UnitTests.Helpers.Authentication.QueriesHelper;

namespace PharmaGOBackend.UnitTests.Systems.Authentication.Queries
{
    public class TestLoginQueryHandler
    {
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly Mock<IClientRepository> _mockClientRepository;

        public TestLoginQueryHandler()
        {
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            _mockClientRepository = MockClientRepository.GetClientRepository();
        }

        //TODO: Encrypt password for validation in handler
        //[Fact]
        //public async Task Login_OnSuccess_ReturnsAuthenticationResult()
        //{
        //    var handler = new LoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

        //    var result = await handler.Handle(
        //        LoginQueryFactory.GetDefault(),
        //        CancellationToken.None
        //        );

        //    result.IsError.Should().BeFalse();
        //    result.Value.Should().BeOfType<AuthenticationResult>();
        //}

        [Fact]
        public async Task Login_PasswordNotInformed_ReturnsInvalidCredentialException()
        {

            var handler = new LoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

            var result = await handler.Handle(
                LoginQueryFactory.GetWithoutPassword(),
                CancellationToken.None
                );

            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
        }

        [Fact]
        public async Task Login_EmailNotInformed_ReturnsInvalidCredentialException()
        {

            var handler = new LoginQueryHandler(_mockJwtTokenGenerator.Object, _mockClientRepository.Object);

            var result = await handler.Handle(
                LoginQueryFactory.GetWithoutEmail(),
                CancellationToken.None
                );

            result.IsError.Should().BeTrue();
            result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
        }
    }
}
