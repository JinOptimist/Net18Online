using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IDndClassRepository<T> : IBaseRepository<T>
        where T : IDNDData
    {
        IEnumerable<T> GetMostPopular();
    }
}
