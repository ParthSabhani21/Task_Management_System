using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Domain.RequestModel;

public record TaskRequestModel
{
    public string? TaskName { get; set; }

    public string? TaskDescription { get; set; }

    public Priorities? Priority { get; set; }

    public long AssignedUserId { get; set; }

    public Status? TaskStatus { get; set; }
}
