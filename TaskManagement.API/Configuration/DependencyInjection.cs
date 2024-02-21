using FluentValidation;
using TaskManagement.Core.Contract;
using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Core.Domain.Validation;
using TaskManagement.Core.Service;
using TaskManagement.Infra.Contract;
using TaskManagement.Infra.Repository;

namespace TaskManagement.API.Configuration;

public static class DependencyInjection
{
    public static void AddDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IValidator<UserRequestModel>, UserValidation>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();

        services.AddTransient<ITaskRepository, TaskRepository>();
        services.AddTransient<ITaskService, TaskService>();

        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<ICommentService, CommentService>();

        services.AddTransient<ITaskHistoryRepository, TaskHistoryRepository>();
        services.AddTransient<ITaskHistoryService, TaskHistoryService>();
    }
}
