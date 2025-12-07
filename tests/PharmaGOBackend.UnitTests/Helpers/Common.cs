namespace PharmaGOBackend.UnitTests.Helpers;

public static class Common
{
    public static string GetRandomName()
    {
        return DateTime.Now.Ticks.ToString();
    }
}