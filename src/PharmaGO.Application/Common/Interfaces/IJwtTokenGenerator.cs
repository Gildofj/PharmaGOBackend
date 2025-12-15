using Microsoft.AspNetCore.Identity;
using PharmaGO.Application.Common.Auth.Constants;

namespace PharmaGO.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    Task<string> GenerateToken(AuthContext context);
}
