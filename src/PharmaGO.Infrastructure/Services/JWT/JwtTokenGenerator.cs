using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PharmaGO.Application.Common.Auth;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Application.Common.Interfaces;
using PharmaGO.Core.Common.Constants;
using PharmaGO.Core.Interfaces.Services;
using PharmaGO.Core.Entities;

namespace PharmaGO.Infrastructure.Services.JWT;

public class JwtTokenGenerator(
    UserManager<IdentityUser<Guid>> userManager,
    IDateTimeProvider dateTimeProvider,
    IOptions<JwtSettings> jwtOptions
) : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public async Task<string> GenerateToken(AuthContext context)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, context.Person.Id.ToString()),
            new(JwtRegisteredClaimNames.GivenName, context.Person.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, context.Person.LastName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(CustomClaims.UserType, nameof(context.UserType))
        };

        if (context.PharmacyId.HasValue)
        {
            claims.Add(new Claim(CustomClaims.PharmacyId, context.PharmacyId.Value.ToString()));
        }
        
        var roles = await userManager.GetRolesAsync(context.User); 
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        var userClaims = await userManager.GetClaimsAsync(context.User);
        claims.AddRange(userClaims);

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}