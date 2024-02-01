using FluentValidation;

namespace PharmaGOBackend.Application.Pharmacies.Commands.CreatePharmacy;

public class CreatePharmacyCommandValidator : AbstractValidator<CreatePharmacyCommand>
{
    public CreatePharmacyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Cnpj).NotEmpty();
    }
}