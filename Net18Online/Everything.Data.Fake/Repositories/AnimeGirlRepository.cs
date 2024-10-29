using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class AnimeGirlRepository : BaseRepository<IGirlData>, IAnimeGirlRepository
    {
        public IEnumerable<IGirlData> GetMostPopular()
        {
            return _entyties
                // .OrderByDescending(x => x.Tags.Contains("Cool") ? 1 : -1)
                .Take(5)
                .ToList();
        }
    }
}
