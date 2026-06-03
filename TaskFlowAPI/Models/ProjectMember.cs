using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlowAPI.Models;

[Table("project_member")]
public class ProjectMember
{
    [Key]
    [Column("project_member_id")]
    public int ProjectMemberId { get; set; }
    
    [Column("project_id")]
    public int ProjectId { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    //Navigating Properties
    public Project Project { get; set; } = null!;
    public User User { get; set; } = null!;
}