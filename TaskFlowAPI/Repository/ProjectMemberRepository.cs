using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Models;
using TaskFlowAPI.Data;
using TaskFlowAPI.Repository.IRepository;

namespace TaskFlowAPI.Repository;

public class ProjectMemberRepository : IProjectMemberRepository
{
    private readonly AppDbContext _context;

    public ProjectMemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetProjects(int userId)
    {
        return await _context.ProjectMembers
            .Where(pm => pm.UserId == userId)
            .Include(pm => pm.Project)
            .Select(pm => pm.Project)
            .ToListAsync();
    }

    public async Task<ProjectMember> AddProjectMember(ProjectMember projectMember)
    {
        _context.ProjectMembers.Add(projectMember);
        await _context.SaveChangesAsync();
        return projectMember;
    }

    public async Task<ProjectMember> RemoveProjectMember(ProjectMember projectMember)
    {
        var existing = await _context.ProjectMembers
            .FirstOrDefaultAsync(x =>
                x.ProjectId == projectMember.ProjectId &&
                x.UserId == projectMember.UserId);

        if (existing == null)
            return null;

        _context.ProjectMembers.Remove(existing);
        await _context.SaveChangesAsync();

        return existing;
    }

    public async Task<int> RemoveProjectMembersByProjectId(int projectId)
    {
        var members = await _context.ProjectMembers
            .Where(x => x.ProjectId == projectId)
            .ToListAsync();

        _context.ProjectMembers.RemoveRange(members);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> RemoveProjectMembersByUserId(int userId)
    {
        var members = await _context.ProjectMembers
            .Where(x => x.UserId == userId)
            .ToListAsync();

        _context.ProjectMembers.RemoveRange(members);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<ProjectMember>> GetProjectMembersByProjectId(int projectId)
    {
        return await _context.ProjectMembers
            .Where(x => x.ProjectId == projectId)
            .Include(x => x.User)
            .ToListAsync();
    }
}