using TaskManagement.Core.Domain.ResponseModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Infra.Contract;

public interface ITaskRepository
{
    Task<Tasks> AddTaskAsync(Tasks task);

    Tasks FindTaskByName(string taskName);

    Tasks FindTaskById(long id);

    Task UpdateTaskAsync(Tasks task);

    List<TaskResponseModel> GetTasks();

    List<TaskResponseModel> GetTasksByStatus(Status status);

    List<TaskResponseModel> GetTasksByPriority(Priorities priority);

    List<TaskResponseModel> GetTasksByAssignedUserId(long userId);
}
