using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
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

    private static int Random_OTP()
    {
        int _min = 0000;
        int _max = 9999;

        Random random = new Random();
        int OTP = random.Next(_min, _max);

        return OTP;
    }

    private int generateOTP = Random_OTP();

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public string GenerateToken(User user)
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

    private User UserAuthentication(string email, string password)
    {
        var user = _userRepository.FindEmailAsync(email).Result;

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
        var user = _userRepository.FindUserByUserName(userRequestModel.UserName);
        if (user != null)
        {
            throw new Exception("User Already Exists");
        }

        // Field Validation
        var validation = new UserValidation();
        var checkFileds = validation.Validate(userRequestModel);
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

    public string LoginUser(string email, string password)
    {
        var user = UserAuthentication(email, password);

        var token = GenerateToken(user);

        return token;

    }

    public async Task<int> SendEmailAsync(long id, string reciverEmail)
    {
        //await _userRepository.FindEmailAsync(reciverEmail);

        var senderMail = _configuration.GetSection("EmailConfig:Email").Value;
        var sederPassword = _configuration.GetSection("EmailConfig:Password").Value;
        var subject = "Email Verification";
        var body = $"This is your Verification OTP = {generateOTP}";

        try
        {
            using var smtp = new SmtpClient(_configuration.GetSection("EmailConfig:Host").Value);
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(senderMail, sederPassword);
            smtp.EnableSsl = true;

            MailMessage mailMessage = new MailMessage(senderMail, reciverEmail, subject, body);
            //StringBuilder data = new StringBuilder();
            //data.AppendLine("1,Parth");
            //data.AppendLine("2,Ankit");
            //using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(data.ToString())))
            //{
            //    //Add a new attachment to the E-mail message, using the correct MIME type
            //    Attachment attachment = new Attachment(stream, new System.Net.Mime.ContentType("text/csv"));
            //    attachment.Name = "data.csv";
            //    mailMessage.Attachments.Add(attachment);
            //}
            smtp.Send(mailMessage);

            var addOTP = new OneTimePassword();
            addOTP.ValidTill = DateTime.Now.AddMinutes(20);
            addOTP.userId = id;
            addOTP.OTP = generateOTP;

            await _userRepository.AddOTP(addOTP);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return generateOTP;
    }

    public async Task CheckOTP(string email, int otp)
    {
        await _userRepository.FindEmailAsync(email);
        var getOTP = _userRepository.GetOTP(otp).Result;


        if(generateOTP == getOTP.OTP) { throw new Exception("OTP Does Not Match"); }

        var userIsVerified = new User().Verify;
    }

}
