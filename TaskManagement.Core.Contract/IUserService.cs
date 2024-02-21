using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Contract;

public interface IUserService
{
    Task<User> AddUserAsync(UserRequestModel userRequestModel);

    string LoginUser(string username, string password);
}
