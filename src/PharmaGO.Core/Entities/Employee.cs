using ErrorOr;
using PharmaGO.Core.Common.Errors;

namespace PharmaGO.Core.Entities;

public class Employee : User
{
    public Guid PharmacyId { get; set; } = Guid.Empty!;
    public virtual Pharmacy Pharmacy { get; set; } = null!;

    public static ErrorOr<Employee> CreateEmployee(
        string firstName,
        string lastName,
        string email,
        string phone,
        Guid pharmacyId
    )
    {
        var employee = new Employee
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            PharmacyId = pharmacyId,
        };

        if (employee.ValidateEmployeeData() is { } error)
        {
            return error;
        }

        return employee;
    }
    
    private Error? ValidateEmployeeData()
    {
        if (string.IsNullOrEmpty(Email))
        {
            return Errors.Authentication.EmailNotInformed;
        }

        if (string.IsNullOrEmpty(FirstName))
        {
            return Errors.Authentication.FirstNameNotInformed;
        }

        if (string.IsNullOrEmpty(LastName))
        {
            return Errors.Authentication.LastNameNotInformed;
        }
        
        if (PharmacyId == Guid.Empty)
        {
            return Errors.Employee.PharmacyIdRequired;
        }

        return null;
    }
}