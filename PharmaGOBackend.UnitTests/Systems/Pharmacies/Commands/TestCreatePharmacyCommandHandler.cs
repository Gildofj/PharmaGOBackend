using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Core.Interfaces.Persistence;
using PharmaGOBackend.Core.Common.Errors;
using PharmaGOBackend.UnitTests.Helpers.CommandsHelper;
using PharmaGOBackend.Application.Pharmacies.Commands.CreatePharmacy;

namespace PharmaGOBackend.UnitTests.Systems.Pharmacies.Commands;

public class TestCreatePharmacyCommandHandler
{
    private readonly Mock<IPharmacyRepository> _mockPharmacyRepository;

    public TestCreatePharmacyCommandHandler()
    {
        _mockPharmacyRepository = new();
    }

    [Fact]
    public async Task Register_OnSuccess_AddPharmacy()
    {
        var handler = new CreatePharmacyCommandHandler(_mockPharmacyRepository.Object);

        var result = await handler.Handle(
            CreatePharmacyCommandFactory.GetDefault(),
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
        var handler = new CreatePharmacyCommandHandler(_mockPharmacyRepository.Object);

        var result = await handler.Handle(
            CreatePharmacyCommandFactory.GetWithoutCnpj(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Pharmacy.CnpjNotInformed);
    }

    [Fact]
    public async Task Register_NameNotInformed_ReturnsNameNotInformedError()
    {
        var handler = new CreatePharmacyCommandHandler(_mockPharmacyRepository.Object);

        var result = await handler.Handle(
            CreatePharmacyCommandFactory.GetWithoutName(),
            default
            );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Pharmacy.NameNotInformed);
    }
}
