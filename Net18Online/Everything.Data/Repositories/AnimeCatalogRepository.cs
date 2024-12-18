using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Everything.Data.Models.SqlRawModels;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IAnimeCatalogRepositoryReal : IAnimeCatalogRepository<AnimeData>
    {
        IEnumerable<AnimeWithShortestReview> GetAnimeWithShortestReview();
    }

    public class AnimeCatalogRepository : BaseRepository<AnimeData>, IAnimeCatalogRepositoryReal
    {
        public AnimeCatalogRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<AnimeWithShortestReview> GetAnimeWithShortestReview()
        {
            var sql = @"
SELECT 
    a.Id AS AnimeId,
    a.Name AS AnimeName,
    a.ImageSrc,
    ISNULL(MIN(CAST(LEN(ar.Review) AS INT)), 0) AS ShortestReviewLength
FROM 
    dbo.Animes AS a
LEFT JOIN 
    dbo.AnimeReviews AS ar ON a.Id = ar.AnimeId
GROUP BY 
    a.Id, a.Name, a.ImageSrc
ORDER BY 
    ShortestReviewLength ASC;";

            var result = _webDbContext
                .Database
                .SqlQueryRaw<AnimeWithShortestReview>(sql)
                .ToList();

            return result;
        }
    }
}
