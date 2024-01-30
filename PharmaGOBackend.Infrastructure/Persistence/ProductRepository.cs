using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Infrastructure.Persistence.Base;

namespace PharmaGOBackend.Infrastructure.Persistence;

public class ProductRepository(PharmaGOContext db) : Repository<Product>(db), IProductRepository
{
}