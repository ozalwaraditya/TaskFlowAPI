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
            try
            {
                var result = await _taskItemService.CreateTask(taskItem);

                return new ResponseDTO
                {
                    IsSuccess = true,
                    Message = "Task created successfully",
                    Response = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        // GET /api/tasks/{taskId}
        [HttpGet("{taskId:int}")]
        public async Task<ResponseDTO> GetTask(int taskId)
        {
            try
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
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        // GET /api/tasks
        [HttpGet]
        public async Task<ResponseDTO> GetAllTasks()
        {
            try
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
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        // PUT /api/tasks/{taskId}
        [HttpPut("{taskId:int}")]
        public async Task<ResponseDTO> UpdateTask(int taskId, [FromBody] TaskItemDto taskItem)
        {
            try
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
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        // DELETE /api/tasks/{taskId}
        [HttpDelete("{taskId:int}")]
        public async Task<ResponseDTO> DeleteTask(int taskId)
        {
            try
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
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        
        [HttpPut("{taskId:int}/assign")]
        public async Task<ResponseDTO> AssignTask(int taskId, [FromBody] AssignTaskDto dto)
        {
            try
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
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        
        [HttpPut("{taskId:int}/status")]
        public async Task<ResponseDTO> UpdateStatus(int taskId, [FromBody] UpdateStatusDto dto)
        {
            try
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
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}