using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface ILoadTestingRepositoryReal : ILoadTestingRepository<MetricData>
    {
        IEnumerable<MetricData> GetWithoutVolumeLoad();
        bool HasSimilarName(string name);
        bool IsNameUniq(string name);
    }
    public class LoadTestingRepository : BaseRepository<MetricData>, ILoadTestingRepositoryReal
    {
        //     private WebDbContext _webDbContext; у родителя теперь

        public LoadTestingRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void DeleteByGuid(Guid Guid)
        {
            var data = Get(Guid);
            Delete(data);
        }

        public MetricData? Get(Guid Guid)
        {
            return _dbSet.FirstOrDefault(x => x.Guid == Guid);
        }

        public IEnumerable<MetricData> GetMostLoaded()
        {
            return GetFinilizeMetric()
                .Take(3)
                .OrderByDescending(x => x.Average)
                .ToList();
        }

        public void UpdateThroughputByGuid(Guid Guid, decimal Throughput)
        {
            var Metric = _dbSet.First(x => x.Guid == Guid);

            Metric.Throughput = Throughput;

            _webDbContext.SaveChanges();
        }

        public void UpdateThroughputById(int Id, decimal Throughput)
        {
            var Metric = _dbSet.First(x => x.Id == Id);

            Metric.Throughput = Throughput;

            _webDbContext.SaveChanges();
        }

        public void UpdateAverageByGuid(Guid Guid, decimal Average)
        {
            var Metric = _dbSet.First(x => x.Guid == Guid);

            Metric.Average = Average;

            _webDbContext.SaveChanges();
        }

        public void UpdateAverageById(int Id, decimal Average)
        {
            var Metric = _dbSet.First(x => x.Id == Id);

            Metric.Average = Average;

            _webDbContext.SaveChanges();
        }
        public void UpdateNameById(int id, string newName)
        {
            var Metric = _dbSet.First(x => x.Id == id);

            Metric.Name = newName;

            _webDbContext.SaveChanges();
        }

        public void UpdateNameByGuid(Guid Guid, string newName)
        {
            var Metric = _dbSet.First(x => x.Guid == Guid);

            Metric.Name = newName;

            _webDbContext.SaveChanges();
        }

        private IQueryable<MetricData> GetFinilizeMetric()
        {
            return _webDbContext
                .Metrics
                .Where(x => !string.IsNullOrEmpty(x.Name));
        }

        public IEnumerable<MetricData> GetWithoutVolumeLoad()
        {
            return _dbSet
                .Where(x => x.LoadVolumeTesting == null)
                .ToList();
        }

        public bool HasSimilarName(string name)
        {
            return _dbSet.Any(x => x.Name.StartsWith(name) || name.StartsWith(x.Name));
        }

        public bool IsNameUniq(string name)
        {
            return !_dbSet.Any(x => x.Name == name);
        }

    }
}
