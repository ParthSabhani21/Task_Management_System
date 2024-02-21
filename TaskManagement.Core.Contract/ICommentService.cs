using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Contract;

public interface ICommentService
{
    Task AddCommentAsync(CommentRequestModel commentRequestModel);

    List<Comment> GetCommentsOnTask(long id);

    List<Comment> GetCommentsByType(CommentType commentType);
}
