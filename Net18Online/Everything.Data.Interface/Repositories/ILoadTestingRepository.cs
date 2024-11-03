using Everything.Data.Interface.Models;
using Everything.Data;

namespace Everything.Data.Interface.Repositories
{
    public interface ILoadTestingRepository<T> : IBaseRepository<T>
        where T : IMetricData
    {
        void Add(IMetricData data);
        void Delete(IMetricData data);
        List<IMetricData> GetAll();
        IMetricData? Get(Guid Guid);
        bool Any();

        IEnumerable<T> GetMostLoaded();

        void UpdateName(Guid Guid, string newName);

        void UpdateImage(Guid Guid, string newThroughput);
    }
}
