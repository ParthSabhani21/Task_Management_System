using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Infra.Contract;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);

    User FindUserByUserName(string username);

    User FindUserById(long id);

    Task<User> FindEmailAsync(string email);

    Task AddOTP(OneTimePassword OTP);

    Task<OneTimePassword> GetOTP(int otp);

}
