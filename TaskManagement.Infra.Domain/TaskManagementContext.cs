using Microsoft.EntityFrameworkCore;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Infra.Domain;

public class TaskManagementContext : DbContext
{
    public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options)
    {
    }

    public DbSet<User> Users {  get; set; }

    public DbSet<Tasks> Tasks { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<TaskHistory> History { get; set; }
    
}
