using AutoMapper;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repository.IRepository;

namespace TaskFlowAPI.Services;

public class ProjectMemberService : IProjectMemberService
{
    private readonly IProjectMemberRepository _repo;
    private readonly IMapper _mapper;

    public ProjectMemberService(IProjectMemberRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<int> CreateProjectMember(int projectId, int userId)
    {
        var entity = new ProjectMember
        {
            ProjectId = projectId,
            UserId = userId
        };

        var result = await _repo.AddProjectMember(entity);
        return result.ProjectMemberId;
    }

    public async Task<IList<ProjectDto>> GetProjectsByUserId(int userId)
    {
        var projects = await _repo.GetProjects(userId);
        return _mapper.Map<IList<ProjectDto>>(projects);
    }

    public async Task<bool> RemoveProjectMember(int projectId, int userId)
    {
        var entity = new ProjectMember
        {
            ProjectId = projectId,
            UserId = userId
        };

        var result = await _repo.RemoveProjectMember(entity);
        return result != null;
    }

    public async Task<int> RemoveProjectMembersByProjectId(int projectId)
    {
        return await _repo.RemoveProjectMembersByProjectId(projectId);
    }

    public async Task<int> RemoveProjectMembersByUserId(int userId)
    {
        return await _repo.RemoveProjectMembersByUserId(userId);
    }

    public async Task<List<ProjectMemberDto>> GetProjectMembersByProjectId(int projectId)
    {
        var response = await _repo.GetProjectMembersByProjectId(projectId);
        return _mapper.Map<List<ProjectMemberDto>>(response);
    }
}