using AutoMapper;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Repository.IRepository;

namespace TaskFlowAPI.Services;

public interface IProjectService
{
    Task<IList<ProjectDto>> GetAllProjects();
    Task<ProjectDto> CreateProject(ProjectDto projectDto, int currentUserId);
    Task<ProjectDto> GetProjectByProjectId(int projectId);
    Task<IList<ProjectDto>> GetProjectByUserId(int userId);
}