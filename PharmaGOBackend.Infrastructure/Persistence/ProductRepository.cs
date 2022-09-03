using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    private readonly PharmaGOContext _db;
    public ProductRepository(PharmaGOContext db)
    {
        _db = db;
    }

  public Product Add(Product product)
  {
    _db.Product.Add(product);
    _db.SaveChanges();
    return product;
  }

  public Product Update(Product product)
  {
    _db.Product.Update(product);
    _db.SaveChanges();
    return product;
  }

  public Product? Get(Guid id)
  {
    return _db.Product
        .Where(p => p.Id == id)
        .FirstOrDefault();
  }

  public List<Product> GetAll(Guid pharmacyId) {
        return _db.Product
                    .Where(p => p.PharmacyId == pharmacyId)
                    .ToList();
    }
}