using MediatR;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;

namespace PharmaGO.Application.Pharmacies.Queries.ListPharmacies;

public class ListPharmaciesQueryHandler(IPharmacyRepository pharmacyRepository)
    : IRequestHandler<ListPharmaciesQuery, List<Pharmacy>>
{
    public async Task<List<Pharmacy>> Handle(ListPharmaciesQuery query, CancellationToken cancellationToken)
    {
         return (await pharmacyRepository.GetAllAsync()).ToList();
    }
}