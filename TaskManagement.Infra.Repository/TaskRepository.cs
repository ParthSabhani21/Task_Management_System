using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Core.Domain.ResponseModel;
using TaskManagement.Infra.Contract;
using TaskManagement.Infra.Domain;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Infra.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagementContext _context;

    //private string[] priorities = { "Urgent", "High", "Low" };

    //private string[] status = { "To Do", "In Progress", "Done" };

    public TaskRepository(TaskManagementContext context)
    {
        _context = context;
    }

    public async Task<Tasks> AddTaskAsync(Tasks task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public Tasks FindTaskByName(string taskName)
    {
        var task = _context.Tasks.Where(t => t.TaskName == taskName).FirstOrDefault();

        return task;
    }

    public Tasks FindTaskById(long id)
    {
        var task = _context.Tasks.Where(x => x.TaskId == id).FirstOrDefault();/* FirstOrDefault(t => t.TaskId == id);*/
        if (task==null)
        {
            throw new Exception($"Task {id} Not Found");
        }
        else
        {
            return task;
        }   
    }

    public async Task UpdateTaskAsync(Tasks task)
    {

        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public List<TaskResponseModel> GetTasks()
    {
        var task = _context.Tasks.Select(x => new TaskResponseModel
        {
            TaskId = x.TaskId,
            TaskName = x.TaskName,
            TaskDescription = x.TaskDescription,
            TaskStatus = x.TaskStatus.Value,
            Priority = x.Priority.Value,
            CreatedOn = x.CreatedOn,
            DueDate = x.DueDate,
        }).ToList();

        return task;
    }

    public List<TaskResponseModel> GetTasksByStatus(Status status)
    {
        var task = _context.Tasks.Where(t => t.TaskStatus == status).Select(x => new TaskResponseModel
        {
            TaskId = x.TaskId,
            TaskName = x.TaskName,
            TaskDescription = x.TaskDescription,
            AssignedUserId = x.AssignedUserId,
            Priority = x.Priority.Value,
            CreatedOn = x.CreatedOn,
            DueDate = x.DueDate,
        }).ToList();

        if(task.Count == 0)
        {
            throw new Exception($" There are No Task with {status} Available");
        }

        return task;
    }

    public List<TaskResponseModel> GetTasksByPriority(Priorities priority)
    {
        var task = _context.Tasks.Where(t => t.Priority == priority).Select(x => new TaskResponseModel
        {
            TaskId = x.TaskId,
            TaskName = x.TaskName,
            TaskDescription = x.TaskDescription,
            AssignedUserId = x.AssignedUserId,
            TaskStatus = x.TaskStatus.Value,
            CreatedOn = x.CreatedOn,
            DueDate = x.DueDate
        }).ToList();

        if (task.Count == 0)
        {
            throw new Exception($" There are No Task with {priority} Available");
        }

        return task;
    }

    public List<TaskResponseModel> GetTasksByAssignedUserId(long userId)
    {
        var task = _context.Tasks.Where(u => u.AssignedUserId == userId).Select(x => new TaskResponseModel
        {
            TaskId = x.TaskId,
            TaskName = x.TaskName,
            TaskDescription = x.TaskDescription,
            TaskStatus = x.TaskStatus.Value,
            Priority = x.Priority.Value,
            CreatedOn = x.CreatedOn,
            DueDate = x.DueDate,
        }).ToList();

        if (task.Count == 0)
        {
            throw new Exception($"UserId {userId} haven't Assigned any Task");
        }

        return task;
    }

}
