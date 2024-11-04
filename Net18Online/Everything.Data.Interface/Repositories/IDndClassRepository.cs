using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IDndClassRepository<T> : IBaseRepository<T>
        where T : IDNDData
    {
        IEnumerable<T> GetMostPopular();

        void UpdateName(int id, string newName);

        void UpdateImage(int id, string url);
    }
}
