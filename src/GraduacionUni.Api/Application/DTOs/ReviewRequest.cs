namespace GraduacionUni.Api.Application.DTOs;

public sealed record CreateReviewRequest(string Comment);
public sealed record ReviewResponse(
    int Id,
    int ProjectId,
    int TutorId,
    string Comment,
    string Status,
    DateTime CreatedAt);

public sealed record UpdateStatusRequest(string Status);