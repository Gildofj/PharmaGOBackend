using PharmaGOBackend.Application.Commands.RegisterPharmacy;

namespace PharmaGOBackend.UnitTests.Helpers.CommandsHelper;

public static class RegisterPharmacyCommandFactory
{
    public static RegisterPharmacyCommand GetDefault()
    {
        return new RegisterPharmacyCommand(
            Common.GetRandomName(),
            "15.041.127/0001-26"
            );
    }

    public static RegisterPharmacyCommand GetWithoutName()
    {
        return new RegisterPharmacyCommand(
            "",
            "15.041.127/0001-26"
            );
    }

    public static RegisterPharmacyCommand GetWithoutCnpj()
    {
        return new RegisterPharmacyCommand(
            Common.GetRandomName(),
            ""
            );
    }
}
