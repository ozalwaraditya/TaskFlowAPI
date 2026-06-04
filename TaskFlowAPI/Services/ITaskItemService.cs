using TaskFlowAPI.DTOs;
using TaskFlowAPI.Enums;

namespace TaskFlowAPI.Services;

public interface ITaskItemService
{
    Task<IList<TaskItemDto>> GetAllTasks(int userId);
    Task<TaskItemDto> GetTaskItem(int taskItemId);
    Task<TaskItemDto> CreateTask(TaskItemDto taskItemDto);
    Task<TaskItemDto> UpdateTask(TaskItemDto taskItemDto);
    Task<bool> DeleteTask(int taskItemId);
    Task<TaskItemDto?> AssignTaskAsync(int taskId, int userId);
    Task<TaskItemDto?> UpdateStatusAsync(int taskId, TaskItemStatus status);
}