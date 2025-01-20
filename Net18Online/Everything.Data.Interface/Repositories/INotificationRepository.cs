using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface INotificationRepository<T> : IBaseRepository<T>
        where T : INotificationData
    {
    }
}
