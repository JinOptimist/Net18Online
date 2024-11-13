using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IUserRepositry<T> : IBaseRepository<T>
        where T : IUser
    {
    }
}
