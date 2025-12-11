using PharmaGO.Application.Employees.Queries.Login;

namespace PharmaGO.UnitTests.Helpers.QueriesHelper;

public class EmployeeLoginQueryFactory
{
    public static EmployeeLoginQuery GetDefault()
    {
        return new EmployeeLoginQuery(
            "teste@teste.com",
            "123"
        );
    }

    public static EmployeeLoginQuery GetWithoutEmail()
    {
        return new EmployeeLoginQuery(
            "",
            "123"
        );
    }

    public static EmployeeLoginQuery GetWithoutPassword()
    {
        return new EmployeeLoginQuery(
            "teste@teste.com",
            ""
        );
    }

    public static EmployeeLoginQuery GetWithWrongPassword()
    {
        return new EmployeeLoginQuery(
            "teste@teste.com",
            "111"
        );
    }
}