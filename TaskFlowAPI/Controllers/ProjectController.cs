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
    public class ProjectController(IProjectService projectService, ILogger<ProjectController> logger) : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger = logger;
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto projectDto)
        {
            string? userIdClaim = User.FindFirstValue(ClaimTypes.Sid);

            if (!int.TryParse(userIdClaim, out int userId))
            {
                _logger.LogWarning("Unauthorized project creation attempt. Invalid SID claim.");
                return Unauthorized(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Unauthorized"
                });
            }
            _logger.LogInformation("User {UserId} is creating a project with name {ProjectName}", userId, projectDto.ProjectName);

            var project = await projectService.CreateProject(projectDto, userId);
            
            if (project == null)
            {
                return NotFound(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Project not found"
                });
            }

            _logger.LogInformation("Project {ProjectId} created successfully by User {UserId}", project.ProjectName, userId);
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
        [Authorize(Roles = "Admin, Manager")]
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