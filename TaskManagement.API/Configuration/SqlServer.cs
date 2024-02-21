using Microsoft.EntityFrameworkCore;
using TaskManagement.Infra.Domain;

namespace TaskManagement.API.Configuration;

public static class SqlServer
{
    public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        {
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<TaskManagementContext>(options => options.UseSqlServer(connectionString,
                                                        s => s.MigrationsAssembly("TaskManagement.Infra.Domain")));
        }
    }
}

