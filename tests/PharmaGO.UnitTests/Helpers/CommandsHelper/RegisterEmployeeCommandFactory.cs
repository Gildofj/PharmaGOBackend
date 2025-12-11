using PharmaGO.Application.Employees.Commands.Register;

namespace PharmaGO.UnitTests.Helpers.CommandsHelper;

public static class RegisterEmployeeCommandFactory
{
    public static RegisterEmployeeCommand GetDefault()
    {
        return new RegisterEmployeeCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            "123",
            Guid.Empty
        );
    }

    public static RegisterEmployeeCommand GetWithoutFirstName()
    {
        return new RegisterEmployeeCommand(
            "",
            Common.GetRandomName(),
            "teste@teste.com",
            "123",
            Guid.Empty
        );
    }

    public static RegisterEmployeeCommand GetWithoutLastName()
    {
        return new RegisterEmployeeCommand(
            "Teste",
            "",
            "teste@teste.com",
            "123",
            Guid.Empty
        );
    }

    public static RegisterEmployeeCommand GetWithoutEmail()
    {
        return new RegisterEmployeeCommand(
            "Teste",
            Common.GetRandomName(),
            "",
            "123",
            Guid.Empty
        );
    }

    public static RegisterEmployeeCommand GetWithoutPassword()
    {
        return new RegisterEmployeeCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            "",
            Guid.Empty
        );
    }
}