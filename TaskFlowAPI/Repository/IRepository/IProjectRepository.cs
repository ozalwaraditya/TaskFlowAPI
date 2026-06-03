namespace TaskFlowAPI.Repository.IRepository;

public interface IProjectRepository
{
    Task<Project?> CreateProject(Project project);
    Task<Project?> GetProject(int projectId);
    Task<IList<Project>> GetAllProjects();
}