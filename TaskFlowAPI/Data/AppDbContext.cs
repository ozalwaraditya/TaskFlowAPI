using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Enums;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Users> Users => Set<Users>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>(entity => 
        {
            entity.HasKey(p => p.ProjectId);
            entity.Property(p => p.ProjectName).IsRequired().HasMaxLength(200);
            entity.Property(p => p.ProjectDescription).HasColumnType("text");
            entity.Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(p => p.UserId);
            entity.Property(p=>p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
        );
    }
}