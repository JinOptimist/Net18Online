using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IAnimeReviewRepositoryReal : IAnimeReviewRepository<AnimeReviewData>
    {
        IEnumerable<AnimeReviewData> GetAll();
        void LinkAnime(int animeReviewId, int animeId);
    }

    public class AnimeReviewsRepository : BaseRepository<AnimeReviewData>, IAnimeReviewRepositoryReal
    {
        public AnimeReviewsRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<AnimeReviewData> GetAll()
        {
            return _dbSet
                .ToList();
        }

        public void LinkAnime(int animeReviewId, int animeId)
        {
            var anime = _webDbContext.Animes.First(x => x.Id == animeId);
            var animeReview = _dbSet.First(x => x.Id == animeReviewId);

            anime.Reviews.Add(animeReview);

            _webDbContext.SaveChanges();
        }
    }
}
