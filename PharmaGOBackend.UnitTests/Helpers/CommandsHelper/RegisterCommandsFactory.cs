using PharmaGOBackend.Application.Commands.Register;

namespace PharmaGOBackend.UnitTests.Helpers.Authentication.CommandsHelper;

public static class RegisterCommandsFactory
{
    public static RegisterCommand GetDefault()
    {
        return new RegisterCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            "123"
            );
    }

    public static RegisterCommand GetWithoutFirstName()
    {
        return new RegisterCommand(
            "",
            Common.GetRandomName(),
            "teste@teste.com",
            "123"
        );
    }

    public static RegisterCommand GetWithoutLastName()
    {
        return new RegisterCommand(
            "Teste",
            "",
            "teste@teste.com",
            "123"
        );
    }

    public static RegisterCommand GetWithoutEmail()
    {
        return new RegisterCommand(
            "Teste",
            Common.GetRandomName(),
            "",
            "123"
        );
    }

    public static RegisterCommand GetWithoutPassword()
    {
        return new RegisterCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            ""
        );
    }

    public static RegisterCommand GetWithRepeatedEmail()
    {
        return new RegisterCommand(
            "Teste",
            Common.GetRandomName(),
            "repeated@teste.com",
            "123"
        );
    }
}