using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface ICoffeChatMessageRepositry<T> : IBaseRepository<T>
        where T : ICoffeChatMessageData
    {
    }
}
