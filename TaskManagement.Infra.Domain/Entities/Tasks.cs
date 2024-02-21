using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Infra.Domain.Entities;

public class Tasks : Audit
{
    [Key]
    public long TaskId { get; set; }

    public string TaskName { get; set; }

    public string? TaskDescription { get; set; }

    public string? FilePath { get; set; }

    public Priorities? Priority {  get; set; }

    public long AssignedUserId { get; set; }

    public Status? TaskStatus { get; set; }

    public DateTime DueDate { get; set; }

    public User? User { get; set; }

    [NotMapped]
    public ICollection<Comment> comments { get; set; }

    public Tasks() { }

    public Tasks(string taskName, string taskDescrition, string filePath, Priorities priorities, long assignedUserId, Status status)
    {
        TaskName = taskName;
        TaskDescription = taskDescrition;
        FilePath = filePath;
        Priority = priorities;
        AssignedUserId = assignedUserId;
        TaskStatus = status;
        CreatedOn = DateTime.UtcNow;
        UpdatedOn = DateTime.UtcNow;
        DueDate = DateTime.UtcNow;
        IsDeleted = false;
    }

    public long Delete()
    {
        IsDeleted = true;
        UpdatedOn = DateTime.UtcNow;

        return TaskId;
    }

    public long Update()
    {
        UpdatedOn = DateTime.UtcNow;

        return TaskId;
    }
}

public enum Status
{
    To_Do = 1,
    In_Progress = 2,
    Done = 3
}

public enum Priorities
{
    Urgent = 1,
    High =2,
    Low = 3
}
