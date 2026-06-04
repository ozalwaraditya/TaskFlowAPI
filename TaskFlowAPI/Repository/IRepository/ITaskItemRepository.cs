using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repository.IRepository;

public interface ITaskItemRepository
{
    Task<IList<TaskItem>> GetAllTaskItemsByUserId(int userId);
    Task<TaskItem?> GetTaskItem(int taskItemId);
    Task<TaskItem> AddTask(TaskItem taskItem);
    Task<TaskItem> UpdateTask(TaskItem taskItem);
    Task<bool> DeleteTask(int taskItemId);
}