using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("projects")]
public class Project
{
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
    public string CreatedBy { get; set; } = "";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}