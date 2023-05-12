using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Infrastructure.Persistence.Base;

namespace PharmaGOBackend.Infrastructure.Persistence;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(PharmaGOContext db) : base(db)
    {
    }
}