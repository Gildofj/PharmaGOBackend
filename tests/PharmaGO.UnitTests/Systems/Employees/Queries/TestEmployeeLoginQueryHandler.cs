using PharmaGO.Application.Employees.Common;
using PharmaGO.Application.Employees.Queries.Login;
using PharmaGO.UnitTests.Helpers.UserHelper;
using PharmaGO.UnitTests.Helpers.QueriesHelper;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Infrastructure.Services;

namespace PharmaGO.UnitTests.Systems.Employees.Queries;

public class TestEmployeeLoginQueryHandler
{
    private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator = new();
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepository = new();
    private readonly IPasswordHashingService _passwordHashing = new PasswordHashingService();

    private EmployeeLoginQueryHandler _handler;

    public TestEmployeeLoginQueryHandler()
    {
        _handler = new EmployeeLoginQueryHandler(
            _mockJwtTokenGenerator.Object,
            _mockEmployeeRepository.Object,
            _passwordHashing
        );
    }

    [Fact]
    public async Task Login_OnSuccess_ReturnsAuthenticationResult()
    {
        _mockEmployeeRepository
            .Setup(x => x.GetEmployeeByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(EmployeeFactory.GetDefaultEmployee());
        
        var result = await _handler.Handle(
            EmployeeLoginQueryFactory.GetDefault(),
            default
        );

        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<EmployeeAuthenticationResult>();
    }

    [Fact]
    public async Task Login_PasswordNotInformed_ReturnsInvalidCredentialError()
    {
        _mockEmployeeRepository
            .Setup(x => x.GetEmployeeByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(EmployeeFactory.GetDefaultEmployee());
        
        var result = await _handler.Handle(
            EmployeeLoginQueryFactory.GetWithoutPassword(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
    }

    [Fact]
    public async Task Login_EmailNotInformed_ReturnsInvalidCredentialError()
    {
        var result = await _handler.Handle(
            EmployeeLoginQueryFactory.GetWithoutEmail(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
    }

    [Fact]
    public async Task Login_IncorrectPassword_ReturnsInvalidCredentialError()
    {
        _mockEmployeeRepository
            .Setup(x => x.GetEmployeeByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(EmployeeFactory.GetDefaultEmployee());
        
        var result = await _handler.Handle(
            EmployeeLoginQueryFactory.GetWithWrongPassword(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidCredentials);
    }
}