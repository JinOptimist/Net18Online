using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IAnimeDescriptionRepository<T> : IBaseRepository<T>
        where T : IAnimeDescriptionData
    {
    }
}
