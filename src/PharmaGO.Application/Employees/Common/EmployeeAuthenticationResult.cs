using PharmaGO.Core.Entities;

namespace PharmaGO.Application.Employees.Common;

public record EmployeeAuthenticationResult(Employee Employee, string Token);