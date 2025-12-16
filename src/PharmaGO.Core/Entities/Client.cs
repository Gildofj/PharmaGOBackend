using System.ComponentModel.DataAnnotations;
using ErrorOr;
using PharmaGO.Core.Common.Errors;

namespace PharmaGO.Core.Entities;

public sealed class Client : Person
{
    public string Cpf { get; set; } = null!;

    public static ErrorOr<Client> Create(
        string firstName,
        string lastName,
        string email,
        string phone,
        string cpf
    )
    {
        var employee = new Client
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Cpf = cpf
        };

        if (employee.ValidateClientData() is { } errors)
        {
            return errors;
        }

        return employee;
    }

    private List<Error>? ValidateClientData()
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

        return errors.Count > 0 ? errors : null;
    }
}