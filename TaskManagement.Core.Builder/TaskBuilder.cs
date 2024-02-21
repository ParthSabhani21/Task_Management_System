using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Builder;

public static class TaskBuilder
{
    public static Tasks Build(TaskRequestModel taskRequestModel, string file)
    {
        return new Tasks
        {
            TaskName = taskRequestModel.TaskName,
            TaskDescription = taskRequestModel.TaskDescription,
            TaskStatus = taskRequestModel.TaskStatus,
            AssignedUserId = taskRequestModel.AssignedUserId,
            Priority = taskRequestModel.Priority,
            CreatedOn = DateTime.Now,
            UpdatedOn = DateTime.Now,
            DueDate = DateTime.Now.AddDays(1),
            FilePath = file
        };
    }
}
