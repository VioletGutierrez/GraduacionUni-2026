namespace GraduacionUni.Api.Application.DTOs;

public sealed record LoginRequest(string Email, string Password);
public sealed record LoginResponse(int UserId, string Email, string Role, string Token);
public sealed record RegisterRequest(string Email, string Password, string Role);