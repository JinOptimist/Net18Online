using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortalEverthing.Models.LoadTesting;

namespace Everything.Data.Fake.Repositories
{
    public class LoadTestingRepository : ILoadTestingRepository
    { // Инициализация модели
        private static List<IMetricData> metrics = new();

        public void Add(IMetricData data) { metrics.Add(data); }
        public void Delete(IMetricData data) { metrics.Remove(data); }
        public List<IMetricData> GetAll() { return metrics; }
        public IMetricData? Get(Guid Guid) { return metrics.FirstOrDefault(m => m.Guid == Guid); }
        public bool Any() { return metrics.Any(); }
    }
}