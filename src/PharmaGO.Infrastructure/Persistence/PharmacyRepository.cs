using PharmaGO.Infrastructure.Persistence.Base;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Entities;

namespace PharmaGO.Infrastructure.Persistence;

public class PharmacyRepository(PharmaGOContext db) : Repository<Pharmacy>(db), IPharmacyRepository
{
}