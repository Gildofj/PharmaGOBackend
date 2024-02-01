using PharmaGOBackend.Application.Pharmacies.Commands.CreatePharmacy;

namespace PharmaGOBackend.UnitTests.Helpers.CommandsHelper;

public static class CreatePharmacyCommandFactory
{
    public static CreatePharmacyCommand GetDefault()
    {
        return new CreatePharmacyCommand(
            Common.GetRandomName(),
            "15.041.127/0001-26"
            );
    }

    public static CreatePharmacyCommand GetWithoutName()
    {
        return new CreatePharmacyCommand(
            "",
            "15.041.127/0001-26"
            );
    }

    public static CreatePharmacyCommand GetWithoutCnpj()
    {
        return new CreatePharmacyCommand(
            Common.GetRandomName(),
            ""
            );
    }
}
