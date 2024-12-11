using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IChatMessageRepositry<T> : IBaseRepository<T>
        where T : IChatMessageData
    {
    }
}
