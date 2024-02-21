using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Domain.RequestModel;

public record UserRequestModel
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Password { get; set; }

    public Genders Gender { get; set; }

    public Designations Designation { get; set; }
}
