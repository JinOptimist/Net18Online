using Everything.Data.Interface.Models;
using Everything.Data;

namespace Everything.Data.Interface.Repositories
{
    public interface ILoadTestingRepository<T> : IBaseRepository<T>
        where T : IMetricData
    {
        IEnumerable<T> GetMostLoaded();

        void UpdateNameById(int Id, string newName);
        void UpdateNameByGuid(Guid Guid, string newName);

        void UpdateThroughputById(int Id, decimal Throughput);
        void UpdateThroughputByGuid(Guid Guid, decimal Throughput);
    }
}
