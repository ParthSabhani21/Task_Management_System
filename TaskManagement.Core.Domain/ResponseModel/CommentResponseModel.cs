using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Domain.ResponseModel;

public class CommentResponseModel
{
    public CommentType CommentType { get; set; }

    public string CommentDesciption { get; set; }

    public long UserId { get; set; }

    public long TaskId { get; set; }
}
