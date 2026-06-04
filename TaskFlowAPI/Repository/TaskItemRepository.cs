using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repository.IRepository;

namespace TaskFlowAPI.Repository;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly AppDbContext _dbContext;
    
    public TaskItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<TaskItem>> GetAllTaskItemsByUserId(int userId)
    {
        return await _dbContext.TaskItems
            .Where(t => t.AssignedTo == userId)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskItem(int taskItemId)
    {
        return await _dbContext.TaskItems.FirstOrDefaultAsync(x => x.TaskItemId == taskItemId);
    }

    public async Task<TaskItem> AddTask(TaskItem taskItem)
    {
        _dbContext.TaskItems.Add(taskItem);
        await _dbContext.SaveChangesAsync();
        return taskItem;
    }

    public async Task<TaskItem> UpdateTask(TaskItem taskItem)
    {
        _dbContext.TaskItems.Update(taskItem);
        await _dbContext.SaveChangesAsync();
        return taskItem;
    }

    public async Task<bool> DeleteTask(int taskItemId)
    {
        var rowsAffected = await _dbContext.TaskItems
            .Where(x => x.TaskItemId == taskItemId)
            .ExecuteDeleteAsync();

        return rowsAffected > 0;
    }
}