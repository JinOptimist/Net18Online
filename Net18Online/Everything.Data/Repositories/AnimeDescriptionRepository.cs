using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IAnimeDescriptionRepositoryReal : IAnimeDescriptionRepository<AnimeDescriptionData>
    {
        IEnumerable<AnimeDescriptionData> GetAllWithAnimes();
        void LinkAnime(int animeDescriptionId, int animeId);
    }

    public class AnimeDescriptionsRepository : BaseRepository<AnimeDescriptionData>, IAnimeDescriptionRepositoryReal
    {
        public AnimeDescriptionsRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<AnimeDescriptionData> GetAllWithAnimes()
        {
            return _dbSet
                .Include(x => x.Animes)
                .ToList();
        }

        public void LinkAnime(int animeDescriptionId, int animeId)
        {
            var anime = _webDbContext.Animes.First(x => x.Id == animeId);
            var animeDescription = _dbSet.First(x => x.Id == animeDescriptionId);

            animeDescription.Animes.Add(anime);

            _webDbContext.SaveChanges();
        }
    }
}
