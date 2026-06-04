using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;
        private readonly ILogger<TaskItemController> _logger;
        public TaskItemController(ITaskItemService taskItemService,  ILogger<TaskItemController> logger)
        {
            _taskItemService = taskItemService;
            _logger = logger;
        }

        // POST /api/tasks
        [HttpPost]
        public async Task<ResponseDTO> CreateTask([FromBody] TaskItemDto taskItem)
        {
            _logger.LogInformation("Creating task item {taskItem}", taskItem);
            var result = await _taskItemService.CreateTask(taskItem);
            _logger.LogInformation("Created task item {taskItem}", result);
            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Task created successfully",
                Response = result
            };
        }

        // GET /api/tasks/{taskId}
        [HttpGet("{taskId:int}")]
        public async Task<ResponseDTO> GetTask(int taskId)
        {
            _logger.LogInformation("Getting task item {taskId}", taskId);
            var task = await _taskItemService.GetTaskItem(taskId);

            if (task == null)
            {
                _logger.LogError("Task not found");
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Task not found"
                };
            }

            _logger.LogInformation("Getting task item {taskId}", taskId);
            return new ResponseDTO
            {
                IsSuccess = true,
                Response = task
            };
        }

        // GET /api/tasks
        [HttpGet]
        public async Task<ResponseDTO> GetAllTasks()
        {
            _logger.LogInformation("Getting all tasks");
            string? userIdClaim = User.FindFirstValue(ClaimTypes.Sid);

            if (!int.TryParse(userIdClaim, out int userId))
            {
                _logger.LogError("UserId not found");
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid user id in token."
                };
            }

            var tasks = await _taskItemService.GetAllTasks(userId);
            _logger.LogInformation("Getting all tasks");
            return new ResponseDTO
            {
                IsSuccess = true,
                Response = tasks
            };
        }

        // PUT /api/tasks/{taskId}
        [HttpPut("{taskId:int}")]
        public async Task<ResponseDTO> UpdateTask(int taskId, [FromBody] TaskItemDto taskItem)
        {
            _logger.LogInformation("Updating task item {taskId}", taskId);
            taskItem.TaskItemId = taskId;
            var updatedTask = await _taskItemService.UpdateTask(taskItem);

            _logger.LogInformation("Updated task item {taskId}", updatedTask);
            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Task updated successfully",
                Response = updatedTask
            };
        }

        // DELETE /api/tasks/{taskId}
        [HttpDelete("{taskId:int}")]
        public async Task<ResponseDTO> DeleteTask(int taskId)
        {
            _logger.LogInformation("Deleting task item {taskId}", taskId);
            var deleted = await _taskItemService.DeleteTask(taskId);

            _logger.LogInformation("Deleted task item {taskId}", deleted);
            return new ResponseDTO
            {
                IsSuccess = deleted,
                Message = deleted
                    ? "Task deleted successfully"
                    : "Task not found"
            };
        }

        // PUT /api/tasks/{taskId}/assign
        [HttpPut("{taskId:int}/assign")]
        public async Task<ResponseDTO> AssignTask(int taskId, [FromBody] AssignTaskDto dto)
        {
            _logger.LogInformation("Assigning task item {taskId}", taskId);
            var result = await _taskItemService.AssignTaskAsync(taskId, dto.UserId);

            if (result == null)
            {
                _logger.LogError("Assigning task item {taskId}", taskId);
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Task not found"
                };
            }

            _logger.LogInformation("Assigned task item {taskId}", result);
            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Task assigned successfully",
                Response = result
            };
        }

        // PUT /api/tasks/{taskId}/status
        [HttpPut("{taskId:int}/status")]
        public async Task<ResponseDTO> UpdateStatus(int taskId, [FromBody] UpdateStatusDto dto)
        {
            _logger.LogInformation("Updating task item {taskId}", taskId);
            var result = await _taskItemService.UpdateStatusAsync(taskId, dto.Status);

            if (result == null)
            {
                _logger.LogError("Updating task item {taskId}", taskId);
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Task not found"
                };
            }

            _logger.LogInformation("Updated task item {taskId}", result);
            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Status updated successfully",
                Response = result
            };
        }
    }
}