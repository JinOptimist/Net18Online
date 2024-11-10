using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IEcologyRepositoryReal : IEcologyRepository<EcologyData>
    {
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
    }
}    