using AutoMapper;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Enums;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repository.IRepository;

namespace TaskFlowAPI.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public TaskItemService(ITaskItemRepository taskItemRepository, IProjectRepository projectRepository, IMapper mapper)
    {
        _taskItemRepository = taskItemRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<IList<TaskItemDto>> GetAllTasks(int userId)
    {
        var tasks = await _taskItemRepository.GetAllTaskItemsByUserId(userId);

        return _mapper.Map<IList<TaskItemDto>>(tasks);
    }

    public async Task<TaskItemDto> GetTaskItem(int taskItemId)
    {
        var taskItem = await _taskItemRepository.GetTaskItem(taskItemId);
        return _mapper.Map<TaskItemDto>(taskItem);
    }

    public async Task<TaskItemDto> CreateTask(TaskItemDto taskItemDto)
    {
        var project = await _projectRepository.GetProject(taskItemDto.ProjectId);
        if (project == null)
        {
            throw new Exception("Project not found");
        }
        var taskItem = _mapper.Map<TaskItem>(taskItemDto);
        var taskItemDb = await _taskItemRepository.AddTask(taskItem);
        return _mapper.Map<TaskItemDto>(taskItemDb);
    }

    public async Task<TaskItemDto> UpdateTask(TaskItemDto taskItemDto)
    {
        var project = await _projectRepository.GetProject(taskItemDto.ProjectId);
        if (project == null)
        {
            throw new Exception("Project not found");
            
        }
        var taskItem = _mapper.Map<TaskItem>(taskItemDto);
        var taskItemDb = await _taskItemRepository.UpdateTask(taskItem);
        return _mapper.Map<TaskItemDto>(taskItemDb);
    }

    public async Task<bool> DeleteTask(int taskItemId)
    {
        return await _taskItemRepository.DeleteTask(taskItemId);
    }

    // ASSIGN TASK
    public async Task<TaskItemDto?> AssignTaskAsync(int taskId, int userId)
    {
        var task = await _taskItemRepository.GetTaskItem(taskId);

        if (task == null)
            return null;

        task.AssignedTo = userId;

        var updated = await _taskItemRepository.UpdateTask(task);

        return _mapper.Map<TaskItemDto>(updated);
    }

    // UPDATE STATUS
    public async Task<TaskItemDto?> UpdateStatusAsync(int taskId, TaskItemStatus status)
    {
        var task = await _taskItemRepository.GetTaskItem(taskId);

        if (task == null)
            return null;

        task.Status = status;

        var updated = await _taskItemRepository.UpdateTask(task);

        return _mapper.Map<TaskItemDto>(updated);
    }
}