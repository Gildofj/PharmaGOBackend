using PharmaGO.Application.Clients.Common;
using PharmaGO.Application.Common.Interfaces;
using PharmaGO.UnitTests.Helpers.UserHelper;
using PharmaGO.UnitTests.Helpers.CommandsHelper;
using PharmaGO.Application.Employees.Commands.Register;
using PharmaGO.Application.Employees.Common;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Infrastructure.Services;

namespace PharmaGO.UnitTests.Systems.Employees.Commands;

public class TestRegisterEmployeeCommandHandler : IAsyncLifetime
{
    private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator = new();
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepository = new();
    private readonly Mock<IPharmacyRepository> _mockPharmacyRepository = new();
    private readonly IPasswordHashingService _passwordHashing = new PasswordHashingService();

    private readonly Guid _pharmacyId = Guid.NewGuid();
    private RegisterEmployeeCommandHandler _handler;

    public TestRegisterEmployeeCommandHandler()
    {
        _handler = new RegisterEmployeeCommandHandler(
            _mockJwtTokenGenerator.Object,
            _mockEmployeeRepository.Object,
            _mockPharmacyRepository.Object,
            _passwordHashing
        );
    }

    public Task InitializeAsync()
    {
        var testPharmacies = new List<Pharmacy>
        {
            new() { Id = _pharmacyId, Name = "Test Pharmacy", Cnpj = "12341234123412" }
        };

        _mockPharmacyRepository
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(testPharmacies);

        _mockPharmacyRepository
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => testPharmacies.FirstOrDefault(x => x.Id == id));

        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Register_OnSuccess_AddEmployee()
    {
        var result = await _handler.Handle(
            RegisterEmployeeCommandFactory.GetDefault(_pharmacyId),
            default
        );

        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<EmployeeAuthenticationResult>();

        _mockEmployeeRepository.Verify(
            x => x.AddAsync(It.Is<Employee>(m => m.Id == result.Value.Employee.Id)),
            Times.Once
        );
    }

    [Fact]
    public async Task Register_FirstNameNotInformed_ReturnsFirstNameNotInformedError()
    {
        var result = await _handler.Handle(
            RegisterEmployeeCommandFactory.GetWithoutFirstName(_pharmacyId),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.FirstNameNotInformed);
    }

    [Fact]
    public async Task Register_LastNameNotInformed_ReturnsLastNameNotInformedError()
    {
        var result = await _handler.Handle(
            RegisterEmployeeCommandFactory.GetWithoutLastName(_pharmacyId),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.LastNameNotInformed);
    }

    [Fact]
    public async Task Register_PasswordNotInformed_ReturnsPasswordNotInformedError()
    {
        var result = await _handler.Handle(
            RegisterEmployeeCommandFactory.GetWithoutPassword(_pharmacyId),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.PasswordNotInformed);
    }

    [Fact]
    public async Task Register_EmailNotInformed_ReturnsEmailNotInformedError()
    {
        var result = await _handler.Handle(
            RegisterEmployeeCommandFactory.GetWithoutEmail(_pharmacyId),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.EmailNotInformed);
    }

    [Fact]
    public async Task Register_ExistingEmailInformation_ReturnsDuplicateEmail()
    {
        _mockEmployeeRepository
            .Setup(x => x.GetEmployeeByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(EmployeeFactory.GetDefaultEmployee());

        var result = await _handler.Handle(
            RegisterEmployeeCommandFactory.GetDefault(_pharmacyId),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Employee.DuplicateEmail);

        _mockEmployeeRepository.Verify(
            x => x.AddAsync(It.IsAny<Employee>()),
            Times.Never
        );
    }
}