using TaskFlowAPI.Enums;

namespace TaskFlowAPI.DTOs;

public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }

}