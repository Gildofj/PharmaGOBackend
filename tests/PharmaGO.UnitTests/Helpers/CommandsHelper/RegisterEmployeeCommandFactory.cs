using PharmaGO.Application.Employees.Commands.Register;

namespace PharmaGO.UnitTests.Helpers.CommandsHelper;

public static class RegisterEmployeeCommandFactory
{
    public static RegisterEmployeeCommand GetDefault(Guid pharmacyId)
    {
        return new RegisterEmployeeCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            "123",
            "5548991333748",
            pharmacyId
        );
    }

    public static RegisterEmployeeCommand GetWithoutFirstName(Guid pharmacyId)
    {
        return new RegisterEmployeeCommand(
            "",
            Common.GetRandomName(),
            "teste@teste.com",
            "123",
            "5548991333748",
            pharmacyId
        );
    }

    public static RegisterEmployeeCommand GetWithoutLastName(Guid pharmacyId)
    {
        return new RegisterEmployeeCommand(
            "Teste",
            "",
            "teste@teste.com",
            "123",
            "5548991333748",
            pharmacyId
        );
    }

    public static RegisterEmployeeCommand GetWithoutEmail(Guid pharmacyId)
    {
        return new RegisterEmployeeCommand(
            "Teste",
            Common.GetRandomName(),
            "",
            "123",
            "5548991333748",
            pharmacyId
        );
    }

    public static RegisterEmployeeCommand GetWithoutPassword(Guid pharmacyId)
    {
        return new RegisterEmployeeCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            "",
            "5548991333748",
            pharmacyId
        );
    }

    public static RegisterEmployeeCommand GetWithoutPhone(Guid pharmacyId)
    {
        return new RegisterEmployeeCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            "123",
            "",
            pharmacyId
        );
    }

    public static RegisterEmployeeCommand GetWithEmptyGuidPharmacyId()
    {
        return new RegisterEmployeeCommand(
            "Teste",
            Common.GetRandomName(),
            "teste@teste.com",
            "123",
            "5548991333748",
            Guid.Empty
        );
    }
}