using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Infra.Domain.Entities;

public class Comment : Audit
{
    [Key]
    public long CommentId { get; set; }

    public string CommentDesciption { get; set; }

    public CommentType CommentType { get; set; }

    public long UserId { get; set; }

    public long? TaskId { get; set; }

    public Tasks? Tasks {  get; set; } 

    public Comment() { }

    public Comment(string commentDescription, CommentType _commentType, DateTime createdOn, DateTime updatedOn)
    {
        CommentDesciption = commentDescription;
        CommentType = _commentType;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
        IsDeleted = false;
    }

    public long Delete()
    {
        IsDeleted = true;

        return CommentId;
    }
}

public enum CommentType
{
    Updates = 1,
    Questions = 2,
    Discussion = 3,
    Facilitating_Collaboration = 4
}
