using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/projects")]
public class ProjectMemberController : ControllerBase
{
    private readonly IProjectMemberService _projectMemberService;
    private readonly ILogger<ProjectMemberController> _logger;

    public ProjectMemberController(IProjectMemberService projectMemberService, ILogger<ProjectMemberController> logger)
    {
        _projectMemberService = projectMemberService;
        _logger = logger;
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpPost("{projectId}/members")]
    public async Task<IActionResult> AddMember(int projectId, [FromBody] AddProjectMemberDto request)
    {
        _logger.LogInformation("Adding member {@request}", request);
        var memberId = await _projectMemberService.CreateProjectMember(projectId, request.UserId);

        _logger.LogInformation("Member {@memberId} added", memberId);
        return Ok(new ResponseDTO
        {
            IsSuccess = true,
            Message = "Project member added successfully.",
            Response = memberId
        });
    }

    [HttpGet("{projectId}/members")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> GetMembers(int projectId)
    {
        _logger.LogInformation("Getting members for {projectId}", projectId);
        var members = await _projectMemberService.GetProjectMembersByProjectId(projectId);
        _logger.LogInformation("Getting members for {projectId}", projectId);
        return Ok(new ResponseDTO
        {
            IsSuccess = true,
            Message = "Project members retrieved successfully.",
            Response = members
        });
    }

    [HttpDelete("{projectId}/members/{userId}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> RemoveMember(int projectId, int userId)
    {
        _logger.LogInformation("Removing member {userId}", userId);
        var removed = await _projectMemberService.RemoveProjectMember(projectId, userId);

        if (!removed)
        {
            _logger.LogError("Removing member {userId} failed", userId);
            return NotFound(new ResponseDTO
            {
                IsSuccess = false,
                Message = "Project member not found."
            });
        }

        _logger.LogInformation("Removed member {userId}", userId);
        return Ok(new ResponseDTO
        {
            IsSuccess = true,
            Message = "Project member removed successfully."
        });
    }
}