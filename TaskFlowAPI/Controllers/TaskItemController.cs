using System.Security.Claims;
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

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        // POST /api/tasks
        [HttpPost]
        public async Task<ResponseDTO> CreateTask([FromBody] TaskItemDto taskItem)
        {
            var result = await _taskItemService.CreateTask(taskItem);

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
            var task = await _taskItemService.GetTaskItem(taskId);

            if (task == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Task not found"
                };
            }

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
            string? userIdClaim = User.FindFirstValue(ClaimTypes.Sid);

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid user id in token."
                };
            }

            var tasks = await _taskItemService.GetAllTasks(userId);
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
            taskItem.TaskItemId = taskId;
            var updatedTask = await _taskItemService.UpdateTask(taskItem);

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
            var deleted = await _taskItemService.DeleteTask(taskId);

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
            var result = await _taskItemService.AssignTaskAsync(taskId, dto.UserId);

            if (result == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Task not found"
                };
            }

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
            var result = await _taskItemService.UpdateStatusAsync(taskId, dto.Status);

            if (result == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Task not found"
                };
            }

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Status updated successfully",
                Response = result
            };
        }
    }
}