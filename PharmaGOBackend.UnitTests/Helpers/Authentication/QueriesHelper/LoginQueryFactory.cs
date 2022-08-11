
using PharmaGOBackend.Application.Authentication.Commands.Register;
using PharmaGOBackend.Application.Authentication.Queries.Login;

namespace PharmaGOBackend.UnitTests.Helpers.Authentication.QueriesHelper
{
    public class LoginQueryFactory
    {
        public static LoginQuery GetDefault()
        {
            return new LoginQuery(
                "teste@teste.com",
                "123"
                );
        }

        public static LoginQuery GetWithoutEmail()
        {
            return new LoginQuery(
                "",
                "123"
            );
        }

        public static LoginQuery GetWithoutPassword()
        {
            return new LoginQuery(
                "teste@teste.com",
                ""
            );
        }
    }
}
