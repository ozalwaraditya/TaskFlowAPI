using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlowAPI.Enums;

namespace TaskFlowAPI.Models;

[Table("task_item")]
public class TaskItem
{
    [Key]
    [Column("task_item_id")]
    public int TaskItemId { get; set; }
    
    [Column("title")]
    public string Title { get; set; }

    [Column("status")] 
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Open; // enum 
    
    [Column("project_id")]
    public int ProjectId { get; set; }
    public Project Project { get; set; } //Navigating property

    [Column("assigned_to")]
    public int AssignedTo { get; set; }
    public ICollection<Comment> Comments { get; set; }
}