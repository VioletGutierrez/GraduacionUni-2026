using GraduacionUni.Api.Application.DTOs;
using GraduacionUni.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduacionUni.Api.Controllers;

[ApiController]
[Route("api/projects")]
[Authorize]
public sealed class ProjectsController(AppDbContext db) : ControllerBase
{
    private static readonly string[] ValidStatuses =
        { "Propuesta", "Revisión", "Aprobada", "En desarrollo", "Defensa", "Cerrada" };

    [HttpGet("mine")]
    public async Task<IActionResult> GetMine(CancellationToken ct)
    {
        var email = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(email)) return Unauthorized();

        var projects = await db.Projects
            .AsNoTracking()
            .Where(p => p.Student != null && p.Student.Email == email)
            .Select(p => new { p.Id, p.Title, p.Status })
            .ToListAsync(ct);

        return Ok(projects);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var project = await db.Projects
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.Title,
                p.Description,
                p.Status,
                p.StudentId,
                Reviews = p.Reviews.Select(r => new
                {
                    r.Id,
                    r.Comment,
                    r.Status,
                    r.TutorId,
                    r.CreatedAt
                }).ToList()
            })
            .SingleOrDefaultAsync(ct);

        return project is null ? NotFound() : Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest(new { error = "El título es obligatorio." });

        var project = new Domain.Entities.Project
        {
            Title = request.Title,
            Description = request.Description,
            StudentId = request.StudentId
        };

        db.Projects.Add(project);
        await db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { id = project.Id },
            new { project.Id, project.Title, project.Status });
    }

    [HttpPatch("{id:int}/status")]
    [Authorize(Roles = "Tutor,Coordinador,Administrador")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Status) || !ValidStatuses.Contains(request.Status))
            return BadRequest(new
            {
                error = $"Estado inválido. Válidos: {string.Join(", ", ValidStatuses)}"
            });

        var project = await db.Projects.FindAsync(new object[] { id }, ct);
        if (project is null) return NotFound();

        project.Status = request.Status;
        await db.SaveChangesAsync(ct);

        return Ok(new { project.Id, project.Title, project.Status });
    }
}

public sealed record CreateProjectRequest(string Title, string? Description, int StudentId);