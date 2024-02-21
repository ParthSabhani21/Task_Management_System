using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Contract;
using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Core.Domain.ResponseModel;
using TaskManagement.Infra.Domain.Entities;

namespace TaskManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("addComment")]
    public async Task<IActionResult> AddCommentAsync(CommentRequestModel commentRequestModel)
    {
        await _commentService.AddCommentAsync(commentRequestModel);
        return Ok($"Comment is Added to Task Id {commentRequestModel.TaskId}");
    }

    [HttpGet("getComment/taskId/{id}")]
    public List<Comment> GetCommentsByTask(long id)
    {
        return _commentService.GetCommentsOnTask(id);
    }

    [HttpGet("getComment/commentType/{commentType}")]
    public List<Comment> GetCommentsByType(CommentType commentType)
    {
        return _commentService.GetCommentsByType(commentType);  
    }

}


