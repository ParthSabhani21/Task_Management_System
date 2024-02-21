using FluentValidation;
using TaskManagement.Core.Domain.RequestModel;

namespace TaskManagement.Core.Domain.Validation;

public class TaskValidation : AbstractValidator<TaskRequestModel>
{
    public TaskValidation()
    {
        RuleFor(u => u.TaskName).NotEmpty();
        RuleFor(u => u.TaskDescription).NotEmpty();
        RuleFor(u => u.Priority).NotEmpty();
        RuleFor(u => u.AssignedUserId).NotEmpty();
    }
}
