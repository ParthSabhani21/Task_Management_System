using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Builder;

public static class HistoryBuilder
{
    public static TaskHistory Build(Tasks task, string file)
    {
        return new TaskHistory
        {
            TaskName = task.TaskName,
            Status = task.TaskStatus,
            Priority = task.Priority,
            FilePath = file,
            DateTime = task.UpdatedOn = task.CreatedOn
        };
    }
}
