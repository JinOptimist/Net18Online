using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Interface.Repositories
{
    public interface ILoadUserRepositry<T> : IBaseRepository<T>
        where T : ILoadUserData
    {
    }
}