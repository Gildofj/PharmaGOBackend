namespace PharmaGO.Application.Common.Auth.Constants;

public static class Policies
{
   // Users
   public const string AdminOnly = "AdminOnly"; 
   public const string PharmacyEmployee = "PharmacyEmployee";
   public const string MasterAdminOnly = "MasterAdminOnly";
   public const string ClientOnly = "ClientOnly";
   
   // Claims
   public const string ManageProduct = "Products.Manage";
   public const string ManageEmployees = "Employees.Manage";
}