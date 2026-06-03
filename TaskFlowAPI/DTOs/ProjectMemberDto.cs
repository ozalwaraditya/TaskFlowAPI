namespace TaskFlowAPI.DTOs;

public class ProjectMemberDto
{
    public int ProjectMemberId { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public UserDto User { get; set; }
}