using GraduacionUni.Api.Application.DTOs;
using GraduacionUni.Api.Domain.Entities;
using GraduacionUni.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduacionUni.Api.Controllers;

[ApiController]
[Route("api/projects/{projectId:int}/reviews")]
[Authorize]
public sealed class ReviewsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int projectId, CancellationToken ct)
    {
        var projectExists = await db.Projects.AnyAsync(p => p.Id == projectId, ct);
        if (!projectExists) return NotFound(new { error = "Proyecto no encontrado." });

        var reviews = await db.Reviews
            .AsNoTracking()
            .Where(r => r.ProjectId == projectId)
            .Select(r => new ReviewResponse(
                r.Id, r.ProjectId, r.TutorId, r.Comment, r.Status, r.CreatedAt))
            .ToListAsync(ct);

        return Ok(reviews);
    }

    [HttpPost]
    [Authorize(Roles = "Tutor,Coordinador,Administrador")]
    public async Task<IActionResult> Create(int projectId, [FromBody] CreateReviewRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Comment))
            return BadRequest(new { error = "El comentario es obligatorio." });

        var project = await db.Projects.FindAsync(new object[] { projectId }, ct);
        if (project is null) return NotFound(new { error = "Proyecto no encontrado." });

        // Obtener el usuario autenticado
        var email = User.Identity?.Name;
        var tutor = await db.Users.SingleOrDefaultAsync(u => u.Email == email, ct);
        if (tutor is null) return Unauthorized();

        var review = new Review
        {
            ProjectId = projectId,
            TutorId = tutor.Id,
            Comment = request.Comment
        };

        db.Reviews.Add(review);
        await db.SaveChangesAsync(ct);

        var response = new ReviewResponse(
            review.Id, review.ProjectId, review.TutorId,
            review.Comment, review.Status, review.CreatedAt);

        return CreatedAtAction(nameof(GetAll), new { projectId }, response);
    }
}