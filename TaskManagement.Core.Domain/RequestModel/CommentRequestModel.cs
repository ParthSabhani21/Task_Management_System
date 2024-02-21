using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Domain.RequestModel;

public class CommentRequestModel
{
    public long UserId { get; set; }

    public long TaskId { get; set; }

    public string CommentDesciption { get; set; }

    public CommentType CommentType { get; set; }
}
