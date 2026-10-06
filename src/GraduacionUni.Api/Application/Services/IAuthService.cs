using GraduacionUni.Api.Application.DTOs;

namespace GraduacionUni.Api.Application.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct);
    Task<LoginResponse> RegisterAsync(RegisterRequest request, CancellationToken ct);
}