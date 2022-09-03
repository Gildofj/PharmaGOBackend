using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Infrastructure.Persistence;

public class PharmacyRepository : IPharmacyRepository
{
    public readonly PharmaGOContext _db;
    public PharmacyRepository(PharmaGOContext db)
    {
        _db = db;
    }

    public Pharmacy Add(Pharmacy pharmacy)
    {
        _db.Pharmacy.Add(pharmacy);
        _db.SaveChanges();
        return pharmacy;
    }

    public Pharmacy? Get(Guid id)
    {
       return _db.Pharmacy
            .Where(x => x.Id == id)
            .FirstOrDefault();
    }

    public List<Pharmacy> GetAll()
    {
        return _db.Pharmacy.ToList();
    }

    public Pharmacy Update(Pharmacy pharmacy)
    {
        _db.Pharmacy.Update(pharmacy);
        _db.SaveChanges();
        return pharmacy;
    }
}