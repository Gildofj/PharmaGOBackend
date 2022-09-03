using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    Product Add(Product product);
    Product Update(Product product);
    Product? Get(Guid id);
    List<Product> GetAll(Guid pharmacyId);
}