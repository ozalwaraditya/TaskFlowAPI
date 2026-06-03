using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlowAPI.Models;


[Table("project")]
public class Project
{
    [Key]
    [Column("project_id")]
    public int ProjectId { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("project_name")]
    public string ProjectName { get; set; } = "";

    [Column("project_description")]
    public string? ProjectDescription { get; set; }

    [Required]
    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    public ICollection<TaskItem> TaskItems { get; set; }

    public ICollection<ProjectMember> ProjectMembers { get; set; }

}