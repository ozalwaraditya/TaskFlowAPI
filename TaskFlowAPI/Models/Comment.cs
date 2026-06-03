using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlowAPI.Models;

[Table("comment")]
public class Comment
{
    [Column("comment_id")]
    public int CommentId { get; set; }
    
    [Column("message")] 
    public string Message { get; set; }
    
    [Column("task_item_id")]
    public int TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; } //Navigating property
    
    [Column("user_id")]
    public int UserId { get; set; }
    public User User { get; set; } //Navigating property
  
}