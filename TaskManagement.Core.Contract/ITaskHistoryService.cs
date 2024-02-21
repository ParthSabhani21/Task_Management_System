using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Contract;

public interface ITaskHistoryService
{
    Task<string> UpdateHistoryAsync(Tasks tasks, string file);
}
