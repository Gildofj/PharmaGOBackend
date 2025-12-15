using Microsoft.AspNetCore.Identity;
using PharmaGO.Core.Common.Constants;
using PharmaGO.Core.Entities;

namespace PharmaGO.Application.Common.Auth.Constants;

public sealed class AuthContext
{
   public required Person Person { get; init; }
   public required IdentityUser<Guid> User { get; init; }
   public required UserType UserType { get; init; }
   public Guid? PharmacyId { get; init; }
}