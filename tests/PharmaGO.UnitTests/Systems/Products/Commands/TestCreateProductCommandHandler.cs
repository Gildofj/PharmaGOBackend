using PharmaGO.UnitTests.Helpers.CommandsHelper;
using PharmaGO.Application.Products.Commands.CreateProduct;
using PharmaGO.Core.Common.Errors;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.UnitTests.Systems.Products.Commands;

public class TestCreateProductCommandHandler
{
    private readonly Mock<IProductRepository> _mockProductRepository = new();

    [Fact]
    public async Task Register_OnSuccess_AddProduct()
    {
        var handler = new CreateProductCommandHandler(_mockProductRepository.Object);

        var result = await handler.Handle(
            RegisterProductCommandFactory.GetDefault(),
            default
        );

        _mockProductRepository.Verify(
            x => x.AddAsync(It.Is<Product>(m => m.Id == result.Value.Id)),
            Times.Once
        );
    }

    [Fact]
    public async Task Register_PharmacyIdNotInformed_ReturnsPharmacyIdNotInformedError()
    {
        var handler = new CreateProductCommandHandler(_mockProductRepository.Object);

        var result = await handler.Handle(
            RegisterProductCommandFactory.GetWithoutPharmacyId(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Product.PharmacyIdNotInformed);
    }

    [Fact]
    public async Task Register_NameNotInformed_ReturnsNameNotInformedError()
    {
        var handler = new CreateProductCommandHandler(_mockProductRepository.Object);

        var result = await handler.Handle(
            RegisterProductCommandFactory.GetWithoutName(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Product.NameNotInformed);
    }

    [Fact]
    public async Task Register_AmountIdNotInformed_ReturnsAmountNotInformedError()
    {
        var handler = new CreateProductCommandHandler(_mockProductRepository.Object);

        var result = await handler.Handle(
            RegisterProductCommandFactory.GetWithoutAmount(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Product.AmountNotInformed);
    }

    [Fact]
    public async Task Register_DescriptionOver300Caracteres_ReturnsDescriptionExceededMaxLengthError()
    {
        var handler = new CreateProductCommandHandler(_mockProductRepository.Object);

        var result = await handler.Handle(
            RegisterProductCommandFactory.GetWithOver300CaracteresDescription(),
            default
        );

        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(Errors.Product.DescriptionExceededMaxLength);
    }
}