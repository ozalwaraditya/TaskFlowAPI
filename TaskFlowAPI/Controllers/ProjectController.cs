using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/projects")]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto projectDto)
        {
            string? userIdClaim = User.FindFirstValue(ClaimTypes.Sid);

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Unauthorized"
                });
            }

            var project = await projectService.CreateProject(projectDto, userId);
            
            if (project == null)
            {
                return NotFound(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Project not found"
                });
            }

            return StatusCode(StatusCodes.Status201Created, new ResponseDTO
            {
                IsSuccess = true,
                Message = "Project created successfully",
                Response = project
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            string? userIdClaim = User.FindFirstValue(ClaimTypes.Sid);

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Unauthorized"
                });
            }

            var projects = await projectService.GetProjectByUserId(userId);

            return Ok(new ResponseDTO
            {
                IsSuccess = true,
                Message = "Projects retrieved successfully",
                Response = projects
            });
        }

        [HttpGet("{projectId:int}")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            var project = await projectService.GetProjectByProjectId(projectId);

            if (project == null)
            {
                return NotFound(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Project not found"
                });
            }

            return Ok(new ResponseDTO
            {
                IsSuccess = true,
                Message = "Project retrieved successfully",
                Response = project
            });
        }
    }
}