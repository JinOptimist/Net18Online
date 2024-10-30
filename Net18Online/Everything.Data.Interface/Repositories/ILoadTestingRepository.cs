using Everything.Data.Interface.Models;
using Everything.Data;

namespace Everything.Data.Interface.Repositories
{
    public interface ILoadTestingRepository
    {
        void Add(IMetricData data);
        void Delete(IMetricData data);
        List<IMetricData> GetAll();
        IMetricData? Get(Guid Guid);
        bool Any();
    }
}
