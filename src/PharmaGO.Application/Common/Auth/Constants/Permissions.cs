namespace PharmaGO.Application.Common.Auth.Constants;

public class Permissions
{
    // MasterAdmin
    public const string CreatePharmacy = "CreatePharmacy";
    public const string UpdatePharmacy = "UpdatePharmacy";
    public const string DeletePharmacy = "DeletePharmacy";
    public const string CreateMasterUsers = "CreateMasterUsers";

    // Client
    public const string ClientAccess = "ClientAccess";

    // Employee
    public const string ManageUsers = "ManageUsers";
    public const string ManageProducts = "ManageProducts";
    public const string ManageOrders = "ManageOrders";
    
    public static readonly IReadOnlyCollection<string> All =
    [
        CreateMasterUsers,
        CreatePharmacy,
        UpdatePharmacy,
        DeletePharmacy,
        ManageOrders,
        ManageUsers,
        ManageProducts,
        ClientAccess
    ];
}