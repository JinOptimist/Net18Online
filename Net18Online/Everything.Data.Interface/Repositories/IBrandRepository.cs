using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IBrandRepository<T> : IBaseRepository<T>
        where T : IBrandData
    {
    }
}
