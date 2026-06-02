using TaskFlowAPI.Enums;

namespace TaskFlowAPI.Models;

public class RegisterRequestBody
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}