using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagement.Core.Builder;
using TaskManagement.Core.Contract;
using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Core.Domain.Validation;
using TaskManagement.Infra.Contract;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    const int keySize = 64;
    const int iterations = 350000;
    HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    private string GenerateToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Designation.ToString())

        };
        var tokeOptions = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return tokenString;
    }

    private string HashPasword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(keySize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);

        return Convert.ToHexString(hash);
    }

    private bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }

    private User UserAuthentication(string username, string password)
    {
        var user = _userRepository.FindUser(username);

        bool verify = VerifyPassword(password, user.Hash_Password, user.Salt_Password);

        if (!verify)
        {
            throw new Exception("Invalid Crendentials");
        }
        return user;
    }

    public async Task<User> AddUserAsync(UserRequestModel userRequestModel)
    {
        // Check If UserName Already Exists
        var user = _userRepository.FindUser(userRequestModel.UserName);
        if (user != null)
        {
            throw new Exception("User Already Exists");
        }

        // Field Validation
        UserValidation validations = new UserValidation();
        var checkFileds = validations.Validate(userRequestModel);
        if (!checkFileds.IsValid)
        {
            throw new Exception(checkFileds.Errors.FirstOrDefault().ErrorMessage);
        }

        // Password Hsshing
        var hashPassword = HashPasword(userRequestModel.Password, out byte[] salt);
        //var saltPassword = Convert.ToHexString(salt);

        // Add User
        var newuser = UserBuilder.Build(userRequestModel, salt, hashPassword);

        return await _userRepository.AddUserAsync(newuser);

    }
    
    public string LoginUser(string username, string password)
    {
        var user = UserAuthentication(username, password);

        var token = GenerateToken(user);

        return token;
        
    }
}
