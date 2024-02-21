using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Infra.Contract;

public interface ITaskHistoryRepository
{
    Task<string> UpdateHistoryAsync(TaskHistory taskHistory);
}
