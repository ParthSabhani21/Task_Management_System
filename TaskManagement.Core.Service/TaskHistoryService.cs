using TaskManagement.Core.Builder;
using TaskManagement.Core.Contract;
using TaskManagement.Infra.Contract;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Service;

public class TaskHistoryService : ITaskHistoryService
{
    private readonly ITaskHistoryRepository _taskHistoryRepository;

    public TaskHistoryService(ITaskHistoryRepository taskHistoryRepository)
    {
        _taskHistoryRepository = taskHistoryRepository;
    }

    public async Task<string> UpdateHistoryAsync(Tasks tasks, string file)
    {
        var history = HistoryBuilder.Build(tasks, file);

        await _taskHistoryRepository.UpdateHistoryAsync(history);

        return "Updated";
    }
}
