namespace TaskManagement.Infra.Domain.Entities;

public class Audit
{
    public bool IsDeleted { get; set; }

    public DateTime CreatedOn {  get; set; }

    public DateTime UpdatedOn {  get; set; }
}


