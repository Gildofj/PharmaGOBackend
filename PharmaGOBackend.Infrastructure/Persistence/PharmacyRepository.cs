using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Infrastructure.Persistence.Base;

namespace PharmaGOBackend.Infrastructure.Persistence;

public class PharmacyRepository(PharmaGOContext db) : Repository<Pharmacy>(db), IPharmacyRepository
{
}