using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Services;

public interface IProjectMemberService
{
    Task<int> CreateProjectMember(int projectId, int userId);
    Task<IList<ProjectDto>> GetProjectsByUserId(int userId);
    Task<bool> RemoveProjectMember(int projectId, int userId);
    Task<int> RemoveProjectMembersByProjectId(int projectId);
    Task<int> RemoveProjectMembersByUserId(int userId);
    Task<List<ProjectMemberDto>> GetProjectMembersByProjectId(int projectId);
}