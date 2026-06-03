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
    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // Project Configuration
        // =========================
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(p => p.ProjectId);
            entity.Property(p => p.ProjectName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.ProjectDescription)
                .HasColumnType("text");

            entity.Property(p => p.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // =========================
        // User Configuration
        // =========================
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);

            entity.Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // =========================
        // Relationships
        // =========================

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.TaskItem)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskItemId);

        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Project)
            .WithMany(p => p.TaskItems)
            .HasForeignKey(t => t.ProjectId);
        
        modelBuilder.Entity<ProjectMember>()
            .HasOne(pm => pm.Project)
            .WithMany(p => p.ProjectMembers)
            .HasForeignKey(pm => pm.ProjectId);

        modelBuilder.Entity<ProjectMember>()
            .HasOne(pm => pm.User)
            .WithMany(u => u.ProjectMembers)
            .HasForeignKey(pm => pm.UserId);
        // =========================
        // Seed Users
        // =========================

        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                Username = "admin1",
                Password = "admin123",
                Email = "admin@taskflow.com",
                Role = Role.Admin,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 2,
                Username = "manager1",
                Password = "manager123",
                Email = "manager@taskflow.com",
                Role = Role.Manager,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 3,
                Username = "dev_john",
                Password = "john123",
                Email = "john@taskflow.com",
                Role = Role.Developer,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 4,
                Username = "dev_sarah",
                Password = "sarah123",
                Email = "sarah@taskflow.com",
                Role = Role.Developer,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // =========================
        // Seed Projects
        // =========================

        modelBuilder.Entity<Project>().HasData(
            new Project
            {
                ProjectId = 1,
                ProjectName = "TaskFlow API",
                ProjectDescription = "Backend API for task management",
                CreatedBy = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Project
            {
                ProjectId = 2,
                ProjectName = "Mobile App",
                ProjectDescription = "Flutter mobile application",
                CreatedBy = 1,
                CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // =========================
        // Seed Project Members
        // =========================

        modelBuilder.Entity<ProjectMember>().HasData(
            new ProjectMember
            {
                ProjectMemberId = 1,
                ProjectId = 1,
                UserId = 2
            },
            new ProjectMember
            {
                ProjectMemberId = 2,
                ProjectId = 1,
                UserId = 3
            },
            new ProjectMember
            {
                ProjectMemberId = 3,
                ProjectId = 1,
                UserId = 4
            },
            new ProjectMember
            {
                ProjectMemberId = 4,
                ProjectId = 2,
                UserId = 3
            }
        );

        // =========================
        // Seed Tasks
        // =========================

        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem
            {
                TaskItemId = 1,
                Title = "Setup Authentication",
                Status = TaskItemStatus.Open,
                ProjectId = 1,
                AssignedTo = 3
            },
            new TaskItem
            {
                TaskItemId = 2,
                Title = "Implement JWT",
                Status = TaskItemStatus.InProgress,
                ProjectId = 1,
                AssignedTo = 4
            },
            new TaskItem
            {
                TaskItemId = 3,
                Title = "Create Login Screen",
                Status = TaskItemStatus.Open,
                ProjectId = 2,
                AssignedTo = 3
            }
        );

        // =========================
        // Seed Comments
        // =========================

        modelBuilder.Entity<Comment>().HasData(
            new Comment
            {
                CommentId = 1,
                Message = "Authentication service created.",
                TaskItemId = 1,
                UserId = 3
            },
            new Comment
            {
                CommentId = 2,
                Message = "JWT middleware integrated.",
                TaskItemId = 2,
                UserId = 4
            },
            new Comment
            {
                CommentId = 3,
                Message = "UI design approved.",
                TaskItemId = 3,
                UserId = 3
            }
        );
    }
}