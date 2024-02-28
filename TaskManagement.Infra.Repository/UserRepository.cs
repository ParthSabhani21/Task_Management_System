using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TaskManagement.Infra.Contract;
using TaskManagement.Infra.Domain;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Infra.Repository;

public class UserRepository : IUserRepository
{
    private readonly TaskManagementContext _context;

    public UserRepository(TaskManagementContext context)
    {
        _context = context;
    }

    public async Task<User> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public User FindUserByUserName(string username)
    {
        var user = _context.Users.Where(u => u.UserName == username).FirstOrDefault();

        return user;
    }

    public User FindUserById(long id)
    {
        var user = _context.Users.Where(u => u.UserId == id).FirstOrDefault();

        return user;
    }

    public async Task<User> FindEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
            throw new Exception("Email Does Not Exists");

        return user;
    }

    public async Task AddOTP(OneTimePassword OTP)
    {
        await _context.OTP.AddAsync(OTP);
        await _context.SaveChangesAsync();
    }

    public async Task<OneTimePassword> GetOTP(int otp)
    {
        var getOTP = await _context.OTP.FirstOrDefaultAsync(x => x.OTP == otp);
        if(getOTP == null) { throw new Exception("OTP is Not Generated"); }
        
        return getOTP;
    }

}
