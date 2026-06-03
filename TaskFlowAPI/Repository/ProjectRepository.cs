using TaskFlowAPI.Data;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repository.IRepository;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace TaskFlowAPI.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _dbContext;

    public ProjectRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Project?> CreateProject(Project project)
    {
        if (project == null)
            return null;
        var projectEntity = await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();
        return projectEntity.Entity;
    }

    public async Task<Project?> GetProject(int projectId)
    {
        return await _dbContext.Projects.FindAsync(projectId);
    }

    public async Task<IList<Project>> GetAllProjects()
    {
        return await _dbContext.Projects.ToListAsync();
    }
}