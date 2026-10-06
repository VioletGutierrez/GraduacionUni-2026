using GraduacionUni.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraduacionUni.Api.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Project>()
            .HasOne(x => x.Student)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.StudentId);
    }
}