using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IAnimeReviewRepository<T> : IBaseRepository<T>
        where T : IAnimeReviewData
    {
    }
}
