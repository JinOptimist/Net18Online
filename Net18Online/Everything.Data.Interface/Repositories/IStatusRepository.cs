using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Interface.Repositories
{
    public interface IStatusRepository<T> : IBaseRepository<T>
        where T : IStatusData
    {
    }
}
