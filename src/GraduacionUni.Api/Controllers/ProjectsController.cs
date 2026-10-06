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
            .Select(p => new { p.Id, p.Title, p.Description, p.Status, p.StudentId })
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
}

public sealed record CreateProjectRequest(string Title, string? Description, int StudentId);