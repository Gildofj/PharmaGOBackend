using PharmaGOBackend.Application.Commands.RegisterClient;

namespace PharmaGOBackend.UnitTests.Helpers.Authentication.CommandsHelper;

public static class RegisterCommandsFactory
{
    public static RegisterCommand GetDefault()
    {
        return new RegisterCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            "123",
            Guid.Empty
            );
    }

    public static RegisterCommand GetWithoutFirstName()
    {
        return new RegisterCommand(
            "",
            Common.GetRandomName(),
            "teste@teste.com",
            "123",
            Guid.Empty
        );
    }

    public static RegisterCommand GetWithoutLastName()
    {
        return new RegisterCommand(
            "Teste",
            "",
            "teste@teste.com",
            "123",
            Guid.Empty
        );
    }

    public static RegisterCommand GetWithoutEmail()
    {
        return new RegisterCommand(
            "Teste",
            Common.GetRandomName(),
            "",
            "123",
            Guid.Empty
        );
    }

    public static RegisterCommand GetWithoutPassword()
    {
        return new RegisterCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            "",
            Guid.Empty
        );
    }

    public static RegisterCommand GetWithRepeatedEmail()
    {
        return new RegisterCommand(
            "Teste",
            Common.GetRandomName(),
            "repeated@teste.com",
            "123",
            Guid.Empty
        );
    }
}