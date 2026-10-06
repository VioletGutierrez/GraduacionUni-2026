namespace GraduacionUni.Api.Domain.Entities;

public sealed class Project
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; } = "Propuesta";
    public int StudentId { get; set; }
    public User? Student { get; set; }
}