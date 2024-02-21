using TaskManagement.Core.Builder;
using TaskManagement.Core.Contract;
using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Core.Domain.ResponseModel;
using TaskManagement.Infra.Contract;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Core.Service;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task AddCommentAsync(CommentRequestModel commentRequestModel)
    { 
        var comment = CommentBuilder.Build(commentRequestModel);

        await _commentRepository.AddCommentAsync(comment); 
    }

    public List<Comment> GetCommentsOnTask(long id)
    {
        return _commentRepository.GetCommentsByTaskId(id);
    }

    public List<Comment> GetCommentsByType(CommentType commentType)
    {
        return _commentRepository.GetCommentsByType(commentType);
    }
}
