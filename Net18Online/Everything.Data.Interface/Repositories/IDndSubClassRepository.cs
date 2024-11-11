using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IDndSubClassRepository<T> : IBaseRepository<T>
        where T : IDndSubClassData
    {
        IEnumerable<T> GetMostPopular();

        void UpdateName(int id, string newName);

        void UpdateImage(int id, string url);

    }
}