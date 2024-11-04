using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IMangaRepositoryReal : IMangaRepository<MangaData>
    {
        IEnumerable<MangaData> GetAllWithCharacters();
        void LinkGirl(int mangaId, int girlId);
    }

    public class MangaRepository : BaseRepository<MangaData>, IMangaRepositoryReal
    {
        public MangaRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<MangaData> GetAllWithCharacters()
        {
            return _dbSet
                .Include(x => x.Characters)
                .ToList();
        }

        public void LinkGirl(int mangaId, int girlId)
        {
            var girl = _webDbContext.Girls.First(x => x.Id == girlId);
            var manga = _dbSet.First(x => x.Id == mangaId);

            manga.Characters.Add(girl);

            _webDbContext.SaveChanges();
        }
    }
}
