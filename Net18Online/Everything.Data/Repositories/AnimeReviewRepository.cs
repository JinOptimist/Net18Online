using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Everything.Data.Repositories
{
    public interface IAnimeReviewRepositoryReal : IAnimeReviewRepository<AnimeReviewData>
    {
        IEnumerable<AnimeReviewData> GetAllWithAnime(int animeId);
        void LinkAnime(int animeReviewId, int animeId);
        string GetUserName(int reviewId);
        void Create(AnimeReviewData reviewData, int currentUserId);
    }

    public class AnimeReviewsRepository : BaseRepository<AnimeReviewData>, IAnimeReviewRepositoryReal
    {
        public AnimeReviewsRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void Create(AnimeReviewData reviewData, int currentUserId)
        {
            var creator = _webDbContext.Users.First(x => x.Id == currentUserId);

            reviewData.Creator = creator;

            Add(reviewData);
        }

        public IEnumerable<AnimeReviewData> GetAllWithAnime(int animeId)
        {
            return _dbSet
                .Where(x => x.Anime.Id == animeId)
                .ToList();
        }

        public string GetUserName(int reviewId)
        {
            return _webDbContext.AnimeReviews
                .Where(z => z.Id == reviewId)
                .Select(z => z.Creator!.Login)
                .FirstOrDefault()!;
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
