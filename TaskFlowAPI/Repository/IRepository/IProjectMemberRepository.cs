using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repository.IRepository;

public interface IProjectMemberRepository
{
    Task<List<Project>> GetProjects(int userId);
    Task<ProjectMember> AddProjectMember(ProjectMember projectMember);
    Task<ProjectMember> RemoveProjectMember(ProjectMember projectMember);
    Task<int> RemoveProjectMembersByProjectId(int projectId);
    Task<int> RemoveProjectMembersByUserId(int userId);
    Task<List<ProjectMember>> GetProjectMembersByProjectId(int projectId);
}