using GraduacionUni.Api.Application.DTOs;
using GraduacionUni.Api.Domain.Entities;
using GraduacionUni.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GraduacionUni.Api.Application.Services;

public sealed class AuthService(
    AppDbContext db,
    IPasswordHasher<User> passwordHasher,
    ITokenService tokenService) : IAuthService
{
    public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct)
    {
        var user = await db.Users.SingleOrDefaultAsync(x => x.Email == request.Email, ct);
        if (user is null) return null;

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed) return null;

        var token = tokenService.CreateToken(user);
        return new LoginResponse(user.Id, user.Email, user.Role, token);
    }

    public async Task<LoginResponse> RegisterAsync(RegisterRequest request, CancellationToken ct)
    {
        var user = new User
        {
            Email = request.Email,
            Role = request.Role,
            PasswordHash = ""
        };
        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

        db.Users.Add(user);
        await db.SaveChangesAsync(ct);

        var token = tokenService.CreateToken(user);
        return new LoginResponse(user.Id, user.Email, user.Role, token);
    }
}