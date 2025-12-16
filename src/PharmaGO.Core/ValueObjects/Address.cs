using ErrorOr;
using PharmaGO.Core.Common;
using PharmaGO.Core.Common.Errors;

namespace PharmaGO.Core.ValueObjects;

public sealed class Address : ValueObject
{
    public string Street { get; init; } = null!;
    public string Number { get; init; } = null!;
    public string Neighborhood { get; init; } = null!;
    public string City { get; init; } = null!;
    public string State { get; init; } = null!;
    public string Country { get; init; } = null!;
    public string ZipCode { get; init; } = null!;

    private Address()
    {
    }

    public static ErrorOr<Address> Create(
        string street,
        string number,
        string neighborhood,
        string city,
        string state,
        string country,
        string zipCode
    )
    {
        if (
            string.IsNullOrWhiteSpace(street) ||
            string.IsNullOrWhiteSpace(zipCode) ||
            string.IsNullOrWhiteSpace(number)
        )
        {
            return Errors.Pharmacy.InvalidAddress;
        }

        var address = new Address
        {
            Street = street,
            Number = number,
            Neighborhood = neighborhood,
            City = city,
            State = state,
            Country = country,
            ZipCode = zipCode
        };
        
        return address;
    }

    protected override IEnumerable<object> GetAtomicValues() =>
        [Street, Number, Neighborhood, City, State, Country, ZipCode];
}