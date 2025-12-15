namespace PharmaGO.Application.Common.Auth.Constants;

public static class EmployeeRoles
{
    public const string Admin = "Admin";
    public const string Employee = "Employee";

    public static readonly IReadOnlyCollection<string> All = [Admin, Employee];
}