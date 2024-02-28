using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Contract;

public interface IUserService
{
    Task<User> AddUserAsync(UserRequestModel userRequestModel);

    string LoginUser(string email, string password);

    Task<int> SendEmailAsync(long id, string reciverEmail);

    Task CheckOTP(string email, int OTP);
}
