using TaskManagement.Infra.Contract;
using TaskManagement.Infra.Domain;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Infra.Repository;

public class TaskHistoryRepository : ITaskHistoryRepository
{
    private readonly TaskManagementContext _context;

    public TaskHistoryRepository(TaskManagementContext context)
    {
        _context = context;
    }

    public async Task<string> UpdateHistoryAsync(TaskHistory taskHistory)
    {
        await _context.History.AddAsync(taskHistory);
        await _context.SaveChangesAsync();

        return "Updated";
    }

}
