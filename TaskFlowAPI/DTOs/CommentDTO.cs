namespace TaskFlowAPI.DTOs;

public class CommentDto
{
    public int CommentId { get; set; }

    public string Message { get; set; } = string.Empty;

    public int TaskItemId { get; set; }

    public int UserId { get; set; }
}