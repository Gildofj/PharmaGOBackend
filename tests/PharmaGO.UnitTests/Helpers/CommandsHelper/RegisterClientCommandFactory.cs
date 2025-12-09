using PharmaGO.Application.Clients.Commands.Register;

namespace PharmaGO.UnitTests.Helpers.CommandsHelper;

public static class RegisterClientCommandFactory
{
    public static RegisterClientCommand GetDefault()
    {
        return new RegisterClientCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            "123"
        );
    }

    public static RegisterClientCommand GetWithoutFirstName()
    {
        return new RegisterClientCommand(
            "",
            Common.GetRandomName(),
            "teste@teste.com",
            "123"
        );
    }

    public static RegisterClientCommand GetWithoutLastName()
    {
        return new RegisterClientCommand(
            "Teste",
            "",
            "teste@teste.com",
            "123"
        );
    }

    public static RegisterClientCommand GetWithoutEmail()
    {
        return new RegisterClientCommand(
            "Teste",
            Common.GetRandomName(),
            "",
            "123"
        );
    }

    public static RegisterClientCommand GetWithoutPassword()
    {
        return new RegisterClientCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            ""
        );
    }
}