using PharmaGO.UnitTests.Helpers.UserHelper;
using PharmaGO.UnitTests.Helpers.CommandsHelper;
using PharmaGO.Application.Employees.Commands.Register;
using PharmaGO.Core.Interfaces.Authentication;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.UnitTests.Systems.Employees.Commands;

public class TestRegisterEmployeeCommandHandler
{
    private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator = new();
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepository = new();

    [Fact]
    public async Task Register_OnSuccess_AddEmployee()
    {
        var handler = new RegisterEmployeeCommandHandler(_mockJwtTokenGenerator.Object, _mockEmployeeRepository.Object);

        var result = await handler.Handle(
            RegisterEmployeeCommandFactory.GetDefault(),
            default
            );

        _mockEmployeeRepository.Verify(
            x => x.AddAsync(It.Is<Employee>(m => m.Id == result.Value.Employee.Id)),
            Times.Once
            );
    }

    [Fact]
    public async Task Register_FirstNameNotInformed_ReturnsFirstNameNotInformedError()
    {
        var handler = new RegisterEmployeeCommandHandler(_mockJwtTokenGenerator.Object, _mockEmployeeRepository.Object);

        var result = await handler.Handle(
            RegisterEmployeeCommandFactory.GetWithoutFirstName(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.FirstNameNotInformed);
    }

    [Fact]
    public async Task Register_LastNameNotInformed_ReturnsLastNameNotInformedError()
    {
        var handler = new RegisterEmployeeCommandHandler(_mockJwtTokenGenerator.Object, _mockEmployeeRepository.Object);

        var result = await handler.Handle(
            RegisterEmployeeCommandFactory.GetWithoutLastName(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.LastNameNotInformed);
    }

    [Fact]
    public async Task Register_PasswordNotInformed_ReturnsPasswordNotInformedError()
    {
        var handler = new RegisterEmployeeCommandHandler(_mockJwtTokenGenerator.Object, _mockEmployeeRepository.Object);

        var result = await handler.Handle(
            RegisterEmployeeCommandFactory.GetWithoutPassword(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.PasswordNotInformed);
    }

    [Fact]
    public async Task Register_EmailNotInformed_ReturnsEmailNotInformedError()
    {
        var handler = new RegisterEmployeeCommandHandler(_mockJwtTokenGenerator.Object, _mockEmployeeRepository.Object);

        var result = await handler.Handle(
            RegisterEmployeeCommandFactory.GetWithoutEmail(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.EmailNotInformed);
    }

    [Fact]
    public async Task Register_ExistingEmailInformation_ReturnsDuplicateEmail()
    {
        _mockEmployeeRepository.Setup(
             x => x.GetEmployeeByEmailAsync(It.IsAny<string>())
             ).ReturnsAsync(EmployeeFactory.GetDefaultEmployee());

        var handler = new RegisterEmployeeCommandHandler(_mockJwtTokenGenerator.Object, _mockEmployeeRepository.Object);

        var result = await handler.Handle(
          RegisterEmployeeCommandFactory.GetDefault(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Employee.DuplicateEmail);
    }

    [Fact]
    public async Task Register_ExistingEmailInformation_NotAddEmployee()
    {
        _mockEmployeeRepository.Setup(
             x => x.GetEmployeeByEmailAsync(It.IsAny<string>())
             ).ReturnsAsync(EmployeeFactory.GetDefaultEmployee());

        var handler = new RegisterEmployeeCommandHandler(_mockJwtTokenGenerator.Object, _mockEmployeeRepository.Object);

        var result = await handler.Handle(
          RegisterEmployeeCommandFactory.GetDefault(),
            default
        );

        _mockEmployeeRepository.Verify(
            x => x.AddAsync(It.IsAny<Employee>()),
            Times.Never
            );
    }
}