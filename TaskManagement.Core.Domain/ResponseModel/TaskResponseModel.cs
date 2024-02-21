using Microsoft.VisualBasic;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Domain.ResponseModel;

public class TaskResponseModel
{
    public long TaskId { get; set; }

    public string TaskName { get; set; }

    public string TaskDescription { get; set; }

    public long AssignedUserId { get; set; }

    public Status TaskStatus { get; set; }

    public Priorities Priority { get; set; } 

    public DateTime CreatedOn {  get; set; }

    public DateTime DueDate {  get; set; }
}
