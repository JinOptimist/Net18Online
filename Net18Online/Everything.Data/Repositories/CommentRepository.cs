using Everything.Data.DataLayerModels;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories;

public interface ICommentRepositoryReal : ICommentRepository<CommentData>
{
    IEnumerable<CommentData> GetCommentsByPostId(int postId);
    CommentsAndPostsByUser GetCommentAuthors(int userId);
}

public class CommentRepository : BaseRepository<CommentData>, ICommentRepositoryReal
{
    public CommentRepository(WebDbContext webDbContext) : base(webDbContext)
    {
    }

    public IEnumerable<CommentData> GetCommentsByPostId(int postId)
    {
        return _dbSet.Where(c => c.PostId == postId).ToList();
    }
    
    public CommentsAndPostsByUser GetCommentAuthors(int userId)
    {
        var user = _webDbContext.Users.FirstOrDefault(u => u.Id == userId);
        //var user = (_dbSet.FirstOrDefault(us => us.UserId == userId))?.User;

        if (user is null)
        {
            return null;
        }  
        
        var comments = user.Comments;
        var posts = user.Ecologies;

        if (comments is null || posts is null)
        {
            return null;
        }

        return new CommentsAndPostsByUser(userId, comments.ToList(), posts.ToList());     
    }
}