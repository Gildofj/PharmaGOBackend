using FluentValidation;

namespace PharmaGO.Application.Employees.Commands.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterEmployeeCommand>
{
  public RegisterCommandValidator()
  {
    RuleFor(x => x.FirstName).NotEmpty();
    RuleFor(x => x.LastName).NotEmpty();
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Password).NotEmpty();
    RuleFor(x => x.PharmacyId).NotEmpty();
  }
}