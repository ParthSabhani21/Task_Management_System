using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Infra.Contract;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);

    User FindUser(string username);
}
