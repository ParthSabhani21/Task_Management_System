using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Builder;

public static class CommentBuilder
{
    public static Comment Build(CommentRequestModel commentRequestModel)
    {
        return new Comment
        {
            UserId = commentRequestModel.UserId,
            TaskId = commentRequestModel.TaskId,
            CommentType = commentRequestModel.CommentType,
            CommentDesciption = commentRequestModel.CommentDesciption,
            CreatedOn = DateTime.Now,
            UpdatedOn = DateTime.Now
        };
    }
}
