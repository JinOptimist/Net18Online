using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories;

public interface ICommentRepositoryReal : ICommentRepository<CommentData>
{
}

public class CommentRepository : BaseRepository<CommentData>, ICommentRepositoryReal
{
    public CommentRepository(WebDbContext webDbContext) : base(webDbContext)
    {
    }


}