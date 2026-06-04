using TaskFlowAPI.Enums;

namespace TaskFlowAPI.DTOs;

public class UpdateStatusDto
{
    public TaskItemStatus Status { get; set; }
}