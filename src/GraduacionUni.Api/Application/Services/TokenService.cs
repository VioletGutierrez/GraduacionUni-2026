using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GraduacionUni.Api.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace GraduacionUni.Api.Application.Services;

public sealed class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? "ClaveDesarrollo_UNI_2024_1234567890_abcdef"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("uid", user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"] ?? "GraduacionUni",
            audience: config["Jwt:Audience"] ?? "GraduacionUniClient",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}