using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Core.Builder;
using TaskManagement.Core.Contract;
using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Core.Domain.ResponseModel;
using TaskManagement.Infra.Contract;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Service;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskHistoryService _taskHistoryService;

    public TaskService(ITaskRepository taskRepository, ITaskHistoryService taskHistoryService)
    {
        _taskRepository = taskRepository;
        _taskHistoryService = taskHistoryService;
    }

    private string UploadFile(string title, IFormFile file)
    {
        var directoryPath = Environment.CurrentDirectory;
        var imageFolderPath = Path.Combine(directoryPath, "Photo");

        Directory.CreateDirectory(imageFolderPath);

        string filePath = Path.Combine(imageFolderPath, $"{title}_" + file.FileName);
        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }

        return filePath;
    }

    public async Task<Tasks> AddTaskAsync(TaskRequestModel taskRequestModel, IFormFile file)
    {
        // Check If Task All-Ready Exist
        var task = _taskRepository.FindTaskByName(taskRequestModel.TaskName);
        if(task != null)
        {
            throw new Exception("Task All-Ready Exists");
        }

        // Add File
        string filePath = UploadFile(taskRequestModel.TaskName, file);

        // Add New Task 
        var newTask = TaskBuilder.Build(taskRequestModel, filePath);

        // History
        var history = await _taskHistoryService.UpdateHistoryAsync(newTask, filePath);

        return await _taskRepository.AddTaskAsync(newTask);
    }

    public async Task UpdateTask(long id, TaskRequestModel taskRequestModel, IFormFile file)
    {
        // file Update
        string filePath = UploadFile(taskRequestModel.TaskName, file);

        var task = _taskRepository.FindTaskById(id);
        task.AssignedUserId = taskRequestModel.AssignedUserId;
        task.TaskDescription = taskRequestModel.TaskDescription;
        task.TaskName = taskRequestModel.TaskName;
        task.TaskStatus = taskRequestModel.TaskStatus;
        task.FilePath = filePath;
        task.UpdatedOn = DateTime.Now;
        task.DueDate = DateTime.Now.AddDays(2);
        
        // History
        var history = _taskHistoryService.UpdateHistoryAsync(task, filePath);

        await _taskRepository.UpdateTaskAsync(task); 
    }

    public List<TaskResponseModel> GetTasks()
    {
        return _taskRepository.GetTasks();
    }

    public List<TaskResponseModel> GetTasksByStatus(Status status)
    {
        return _taskRepository.GetTasksByStatus(status);
    }

    public List<TaskResponseModel> GetTasksByPriority(Priorities priority)
    {
        return _taskRepository.GetTasksByPriority(priority);
    }

    public List<TaskResponseModel> GetTasksByAssignedUserId(long userId)
    {
        return _taskRepository.GetTasksByAssignedUserId(userId);
    }

    public List<Tasks> GetTasksByDueDate()
    {
        return _taskRepository.GetTasksByDueDate();
    }
}
