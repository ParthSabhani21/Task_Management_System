using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Infra.Domain.Entities;

public class TaskHistory
{
    [Key]
    public long Id { get; set; }

    public string TaskName { get; set; }

    public string? FilePath { get; set; }

    public Status? Status { get; set; }

    public Priorities? Priority { get; set; }

    public DateTime DateTime { get; set; }

    public Tasks Task { get; set; }

    public TaskHistory() { }

    public TaskHistory(string taskName, Status status, Priorities priorities, string filePath)
    {
        TaskName = taskName;
        Status = status;
        Priority = priorities;
        FilePath = filePath;
        DateTime = DateTime.UtcNow;
    }
}
