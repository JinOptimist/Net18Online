using Everything.Data.Interface.Models;
using Everything.Data;

namespace Everything.Data.Interface.Repositories
{
    public interface ILoadTestingRepository<T> : IBaseRepository<T>
        where T : IMetricData
    {
        IEnumerable<T> GetMostLoaded();

        void UpdateName(Guid Guid, string newName);

        void UpdateThroughput(Guid Guid, string newThroughput);
    }
}
