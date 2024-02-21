using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Contract;
using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Core.Domain.ResponseModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost("addTask")]
    [Authorize]
    public async Task<IActionResult> AddTaskAsync([FromForm]TaskRequestModel taskRequestModel, IFormFile file)
    {
        await _taskService.AddTaskAsync(taskRequestModel, file);
        return Ok($"Task {taskRequestModel.TaskName} Created Successfully And It is Assigned to User Id {taskRequestModel.AssignedUserId}");
    }

    [HttpPut("updateTask")]
    [Authorize]
    public string UpdateTask(long id, [FromForm] TaskRequestModel taskRequestModel, IFormFile file)
    {
        _taskService.UpdateTask(id, taskRequestModel, file);

        return $"Task {id} is Updated Successfully";
    }

    [HttpGet("getTasks")]
    [Authorize(Roles = "Admin, Manager")]
    public List<TaskResponseModel> GetTasks()
    {
        return _taskService.GetTasks();
    }

    [HttpGet("getTasks/byStatus/{status}")]
    [Authorize(Roles = "Admin, Manager")]
    public List<TaskResponseModel> GetTasksByStatus(Status status)
    {
        return _taskService.GetTasksByStatus(status);
    }

    [HttpGet("getTasks/byPiority/{priority}")]
    [Authorize(Roles = "Admin, Manager")]
    public List<TaskResponseModel> GetTasksByStatus(Priorities priority)
    {
        return _taskService.GetTasksByPriority(priority);
    }

    [HttpGet("getTask/byAssignedUserId/{userId}")]
    [Authorize(Roles = "Admin, Manager")]
    public List<TaskResponseModel> GetTasksByAssignedUserId(long userId)
    {
        return _taskService.GetTasksByAssignedUserId(userId);
    }
}
