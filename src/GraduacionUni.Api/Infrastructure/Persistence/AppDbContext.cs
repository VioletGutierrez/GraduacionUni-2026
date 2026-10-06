using GraduacionUni.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraduacionUni.Api.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Project>()
            .HasOne(x => x.Student)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(x => x.Project)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(x => x.Tutor)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.TutorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}