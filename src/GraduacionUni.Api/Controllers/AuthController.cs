using GraduacionUni.Api.Application.DTOs;
using GraduacionUni.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GraduacionUni.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { error = "Email y contraseña son obligatorios." });

        var result = await authService.LoginAsync(request, ct);
        return result is null ? Unauthorized(new { error = "Credenciales inválidas." }) : Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { error = "Email y contraseña son obligatorios." });

        var result = await authService.RegisterAsync(request, ct);
        return Ok(result);
    }
}