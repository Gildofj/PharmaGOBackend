using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Common.Interfaces.Persistence;

public interface IPharmacyRepository
{
    Pharmacy Add(Pharmacy pharmacy);
    Pharmacy Update(Pharmacy pharmacy);
    Pharmacy? Get(Guid id);
    List<Pharmacy> GetAll();
}