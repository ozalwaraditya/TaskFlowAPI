using System.ComponentModel.DataAnnotations.Schema;
using TaskFlowAPI.Enums;

namespace TaskFlowAPI.Models;

[Table("Users")]
public class User
{
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("username")]
    public string Username { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
    
    [Column("email")]
    public string Email { get; set; }

    [Column("role")] 
    public Role Role { get; set; } = Role.Developer; // Admin, Manager, Developer
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}