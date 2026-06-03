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

    public ProjectMemberController(IProjectMemberService projectMemberService)
    {
        _projectMemberService = projectMemberService;
    }

    [HttpPost("{projectId}/members")]
    public async Task<IActionResult> AddMember(int projectId, [FromBody] AddProjectMemberDto request)
    {
        var memberId = await _projectMemberService.CreateProjectMember(projectId, request.UserId);

        return Ok(new ResponseDTO
        {
            IsSuccess = true,
            Message = "Project member added successfully.",
            Response = memberId
        });
    }

    [HttpGet("{projectId}/members")]
    public async Task<IActionResult> GetMembers(int projectId)
    {
        var members = await _projectMemberService.GetProjectMembersByProjectId(projectId);

        return Ok(new ResponseDTO
        {
            IsSuccess = true,
            Message = "Project members retrieved successfully.",
            Response = members
        });
    }

    [HttpDelete("{projectId}/members/{userId}")]
    public async Task<IActionResult> RemoveMember(int projectId, int userId)
    {
        var removed = await _projectMemberService.RemoveProjectMember(projectId, userId);

        if (!removed)
        {
            return NotFound(new ResponseDTO
            {
                IsSuccess = false,
                Message = "Project member not found."
            });
        }

        return Ok(new ResponseDTO
        {
            IsSuccess = true,
            Message = "Project member removed successfully."
        });
    }
}