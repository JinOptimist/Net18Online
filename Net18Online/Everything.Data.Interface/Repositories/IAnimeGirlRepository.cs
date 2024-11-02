using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IAnimeGirlRepository<T> : IBaseRepository<T>
        where T : IGirlData
    {
        IEnumerable<T> GetMostPopular();

        void UpdateName(int id, string newName);

        void UpdateImage(int id, string url);
    }
}
