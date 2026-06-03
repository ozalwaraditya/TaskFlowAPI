using AutoMapper;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Repository.IRepository;

namespace TaskFlowAPI.Services;

public class ProjectService  :IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectMemberRepository _projectMemberRepository;
    private readonly IMapper _mapper;
    
    public ProjectService(IProjectRepository projectRepository,  IProjectMemberRepository projectMemberRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _projectMemberRepository = projectMemberRepository;
        _mapper = mapper;
    }

    public async Task<IList<ProjectDto>> GetAllProjects()
    {
        var projects = await _projectRepository.GetAllProjects();
        return _mapper.Map<IList<ProjectDto>>(projects);
    }

    public async Task<ProjectDto> CreateProject(ProjectDto projectDto, int currentUserId)
    {
        var project = _mapper.Map<Project>(projectDto);
        project.CreatedBy = currentUserId;
        await _projectRepository.CreateProject(project);
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<ProjectDto> GetProjectByProjectId(int projectId)
    {
        var project = await _projectRepository.GetProject(projectId);
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<IList<ProjectDto>> GetProjectByUserId(int userId)
    {
        var project = await _projectMemberRepository.GetProjects(userId);
        return _mapper.Map<IList<ProjectDto>>(project);
    }
}