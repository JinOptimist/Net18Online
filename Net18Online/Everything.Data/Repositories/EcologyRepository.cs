using Everything.Data.DataLayerModels;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    
    public interface IEcologyRepositoryReal : IEcologyRepository<EcologyData>
    {
        void Create(EcologyData ecology, int currentUserId, int postId);
        IEnumerable<EcologyData>GetAllWithUsersAndComments();
        
        EcologyData FindById(int postId);
        void AddPostToMovedPosts(MovedPostReference movedPost);

    }

    public class EcologyRepository : BaseRepository<EcologyData>, IEcologyRepositoryReal
    {
        public EcologyRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void UpdatePost(int id, string url, string text)
        {
            var ecology = _dbSet.First(e => e.Id == id); 

            ecology.ImageSrc = url;
            ecology.Text = text; 
                
            _webDbContext.SaveChanges();
        }
        
        public EcologyData FindById(int postId)
        {
            return _webDbContext.Ecologies.Find(postId);
        }

        public void AddPostToMovedPosts(MovedPostReference movedPost)
        {
            var movedPostReference = new MovedPostReference
            {
                PostId = movedPost.Id
            };

            _webDbContext.MovedPostReferences.Add(movedPostReference);
            _webDbContext.SaveChanges();
        }
        
        public IEnumerable<EcologyData> GetAllWithUsersAndComments()
        {
            return _dbSet
                .Include(x => x.User)
                .Include(x => x.Comments)
                .ToList();
        }
        
        public void Create(EcologyData ecology, int currentUserId, int postId)
        {
            var creator = _webDbContext.Users.FirstOrDefault(x => x.Id == currentUserId);
        
            var comments = _webDbContext.Comments.Where(x => x.Id == postId);

            ecology.User = creator;
            ecology.Comments = comments;

            Add(ecology);
        }
    }
}    