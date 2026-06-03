using TaskFlowAPI.Enums;

namespace TaskFlowAPI.DTOs;

public class TaskItemDto
{
    public int TaskItemId { get; set; }

    public string Title { get; set; } = string.Empty;

    public TaskItemStatus Status { get; set; }

    public int ProjectId { get; set; }

    public int AssignedTo { get; set; }
}