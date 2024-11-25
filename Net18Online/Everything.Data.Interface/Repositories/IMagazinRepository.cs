using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IMagazinRepository<T> : IBaseRepository<T>
        where T : IMagazinData
    {
    }
}
