using TaskManagement.Core.Domain.ResponseModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Infra.Contract;

public interface ICommentRepository
{
    Task AddCommentAsync(Comment comment);

    List<Comment> GetCommentsByTaskId(long id);

    List<Comment> GetCommentsByType(CommentType commentType);
}
