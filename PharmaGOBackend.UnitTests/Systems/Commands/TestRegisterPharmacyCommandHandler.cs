using PharmaGOBackend.Application.Commands.RegisterPharmacy;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.Core.Common.Errors;
using PharmaGOBackend.UnitTests.Helpers.CommandsHelper;

namespace PharmaGOBackend.UnitTests.Systems.Commands;

public  class TestRegisterPharmacyCommandHandler
{
    private readonly Mock<IPharmacyRepository> _mockPharmacyRepository;

    public TestRegisterPharmacyCommandHandler()
    {
        _mockPharmacyRepository = new();
    }

    [Fact]
    public async Task Register_OnSuccess_AddPharmacy()
    {
        var handler = new RegisterPharmacyCommandHandler(_mockPharmacyRepository.Object);

        var result = await handler.Handle(
            RegisterPharmacyCommandFactory.GetDefault(),
            default
            );

        _mockPharmacyRepository.Verify(
            x => x.AddAsync(It.Is<Pharmacy>(m => m.Id == result.Value.Id)),
            Times.Once
            );
    }

    [Fact]
    public async Task Register_CnpjNotInformed_ReturnsCnpjNotInformedError()
    {
        var handler = new RegisterPharmacyCommandHandler(_mockPharmacyRepository.Object);

        var result = await handler.Handle(
            RegisterPharmacyCommandFactory.GetWithoutCnpj(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Pharmacy.CnpjNotInformed);
    }

    [Fact]
    public async Task Register_NameNotInformed_ReturnsNameNotInformedError()
    {
        var handler = new RegisterPharmacyCommandHandler(_mockPharmacyRepository.Object);

        var result = await handler.Handle(
            RegisterPharmacyCommandFactory.GetWithoutName(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Pharmacy.NameNotInformed);
    }
}
