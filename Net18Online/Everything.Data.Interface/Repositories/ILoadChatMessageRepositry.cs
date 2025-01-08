using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface ILoadChatMessageRepositry<T> : IBaseRepository<T>
        /*тут и в IChatMessageData решила пока ничего не править*/
        where T : IChatMessageData
    {
    }
}
