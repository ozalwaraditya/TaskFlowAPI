using TaskFlowAPI.Enums;

namespace TaskFlowAPI.DTOs;

public class UserDTO
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }

}