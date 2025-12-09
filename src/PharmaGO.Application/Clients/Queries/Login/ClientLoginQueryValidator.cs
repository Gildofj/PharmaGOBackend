using FluentValidation;

namespace PharmaGO.Application.Clients.Queries.Login;

public class ClientLoginQueryValidator : AbstractValidator<ClientLoginQuery>
{
    public ClientLoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}