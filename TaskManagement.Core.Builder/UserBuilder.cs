using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Builder;

public static class UserBuilder
{
    public static User Build(UserRequestModel userRequestModel, byte[] saltPassword, string hashPassword)
    {
        return new User
        {
            UserName = userRequestModel.UserName,
            Email = userRequestModel.Email,
            PhoneNumber = userRequestModel.PhoneNumber,
            Gender = userRequestModel.Gender,
            Designation = userRequestModel.Designation,
            CreatedOn = DateTime.Now,
            UpdatedOn = DateTime.Now,
            Hash_Password = userRequestModel.Password = hashPassword,
            Salt_Password = saltPassword
        };
    }

}
