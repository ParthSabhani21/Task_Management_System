using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Infra.Domain.Entities;

public class OneTimePassword
{
    public long Id { get; set; }

    public int OTP { get; set; }

    public DateTime ValidTill { get; set; }

    public long userId { get; set; }
    public User User {  get; set; }
}
