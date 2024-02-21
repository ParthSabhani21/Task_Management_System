using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Domain.ResponseModel;
using TaskManagement.Infra.Contract;
using TaskManagement.Infra.Domain;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.Infra.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly TaskManagementContext _context;

    public CommentRepository(TaskManagementContext context)
    {
        _context = context;
    }

    public async Task AddCommentAsync(Comment comment)
    {
        var task = _context.Tasks.Any(x => x.TaskId == comment.TaskId);
        var user = _context.Users.Any(x => x.UserId == comment.UserId);
        if (!task)
        {
            throw new Exception($"Task {comment.TaskId} Not Found");
        }else if (!user)
        {
            throw new Exception($"User {comment.UserId} Not Found");
        }

        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public List<Comment> GetCommentsByTaskId(long id)
    {
        var result = _context.Comments.Where(x => x.TaskId == id).ToList();
        
        return result;
    }

    public List<Comment> GetCommentsByType(CommentType commentType)
    {
        return _context.Comments.Where(c => c.CommentType == commentType).ToList();
    }
}
