using FluentValidation;

namespace PharmaGO.Application.Employees.Queries.Login;

public class EmployeeLoginQueryValidator : AbstractValidator<EmployeeLoginQuery>
{
    public EmployeeLoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}