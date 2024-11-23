using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface ILoadUserRepositry<T> : IBaseRepository<T>
        where T : ILoadUserData
    {
    }
}