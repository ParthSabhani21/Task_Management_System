using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Infra.Domain.Entities;

public class User : Audit
{
    [Key]
    public long UserId { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber {  get; set; }

    public Genders Gender { get; set; }

    public Designations Designation { get; set; }

    public string Hash_Password {  get; set; }

    public byte[] Salt_Password { get; set; }

    public User() { }

    public User(string userName, string email, string phone, Genders _genders, Designations _designations, string hashPassword, byte[] saltPassword)
    {
        UserName = userName;
        Email = email;
        PhoneNumber = phone;
        Gender = _genders;
        Designation = _designations;
        Hash_Password = hashPassword;
        Salt_Password = saltPassword;
        CreatedOn = DateTime.UtcNow;
        UpdatedOn = DateTime.UtcNow;
        IsDeleted = false;
    }

    public long Delete()
    {
        IsDeleted = true;
        UpdatedOn = DateTime.UtcNow;

        return UserId;
    }

    public long Update()
    {
        UpdatedOn = DateTime.UtcNow;

        return UserId;
    }
}

public enum Genders
{
    Male = 1,
    Female = 2
}

public enum Designations
{
    Admin = 1,
    Manager = 2,
    Team_Member = 3
}
