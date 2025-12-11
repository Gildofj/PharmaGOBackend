using PharmaGO.Application.Clients.Queries.Login;

namespace PharmaGO.UnitTests.Helpers.QueriesHelper;

public class ClientLoginQueryFactory
{
    public static ClientLoginQuery GetDefault()
    {
        return new ClientLoginQuery(
            "teste@teste.com",
            "123"
        );
    }

    public static ClientLoginQuery GetWithoutEmail()
    {
        return new ClientLoginQuery(
            "",
            "123"
        );
    }

    public static ClientLoginQuery GetWithoutPassword()
    {
        return new ClientLoginQuery(
            "teste@teste.com",
            ""
        );
    }

    public static ClientLoginQuery GetWithWrongPassword()
    {
        return new ClientLoginQuery(
            "teste@teste.com",
            "111"
        );
    }
}