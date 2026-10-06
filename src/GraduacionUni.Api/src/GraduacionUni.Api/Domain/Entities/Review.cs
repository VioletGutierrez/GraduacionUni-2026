namespace GraduacionUni.Api.Domain.Entities;

public sealed class Review
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int TutorId { get; set; }
    public User? Tutor { get; set; }
    public required string Comment { get; set; }
    public string Status { get; set; } = "Pendiente";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}