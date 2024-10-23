using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IAnimeGirlRepository : IBaseRepository<IGirlData>
    {
        IEnumerable<IGirlData> GetMostPopular();
    }
}
