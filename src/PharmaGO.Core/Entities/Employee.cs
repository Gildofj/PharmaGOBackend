using ErrorOr;
using PharmaGO.Core.Common.Errors;

namespace PharmaGO.Core.Entities;

public sealed class Employee : Person
{
    public Guid PharmacyId { get; set; } = Guid.Empty!;
    public Pharmacy Pharmacy { get; set; } = null!;

    public static ErrorOr<Employee> Create(
        Guid identityUserId,
        string firstName,
        string lastName,
        string email,
        string phone,
        Guid pharmacyId
    )
    {
        var employee = new Employee
        {
            IdentityUserId = identityUserId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            PharmacyId = pharmacyId,
        };

        if (employee.ValidateEmployeeData() is { } errors)
        {
            return errors;
        }

        return employee;
    }
    
    private List<Error>? ValidateEmployeeData()
    {
        List<Error> errors = [];
        
        if (string.IsNullOrEmpty(Email))
        {
            errors.Add(Errors.Authentication.EmailNotInformed);
        }

        if (string.IsNullOrEmpty(FirstName))
        {
            errors.Add(Errors.Authentication.FirstNameNotInformed);
        }

        if (string.IsNullOrEmpty(LastName))
        {
            errors.Add(Errors.Authentication.LastNameNotInformed);
        }
        
        if (PharmacyId == Guid.Empty)
        {
            errors.Add(Errors.Employee.PharmacyIdRequired);
        }

        return errors.Count > 0 ? errors : null;
    }
}