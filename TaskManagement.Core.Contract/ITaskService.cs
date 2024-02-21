using Microsoft.AspNetCore.Http;
using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Core.Domain.ResponseModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Contract;

public interface ITaskService
{
    Task<Tasks> AddTaskAsync(TaskRequestModel taskRequestModel, IFormFile formFile);

    Task UpdateTask(long id, TaskRequestModel taskRequestModel, IFormFile file);

    List<TaskResponseModel> GetTasks();

    List<TaskResponseModel> GetTasksByStatus(Status status);

    List<TaskResponseModel> GetTasksByPriority(Priorities priority);

    List<TaskResponseModel> GetTasksByAssignedUserId(long userId);

    List<Tasks> GetTasksByDueDate();
}
