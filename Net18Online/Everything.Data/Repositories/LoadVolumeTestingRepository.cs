using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Repositories
{
    public interface ILoadVolumeTestingRepositoryReal : ILoadVolumeTestingRepository<LoadVolumeTestingData>
    {
        IEnumerable<LoadVolumeTestingData> GetAllWithVolumeMetrics();
        IEnumerable<LoadVolumeTestingData> GetAllWithCreators();
        void LinkMetric(int loadVolumeMetricId, int metriclId);
        void Create(LoadVolumeTestingData loadVolumeTestingData, int currentUserId);
    }

    public class LoadVolumeTestingRepository : BaseRepository<LoadVolumeTestingData>, ILoadVolumeTestingRepositoryReal
    {
        public LoadVolumeTestingRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void Create(LoadVolumeTestingData loadVolumeTestingData, int currentUserId)
        {
            var creator = _webDbContext.LoadUsers.First(x => x.Id == currentUserId);

            loadVolumeTestingData.LoadUserDataCreator = creator;

            Add(loadVolumeTestingData);
        }

        public IEnumerable<LoadVolumeTestingData> GetAllWithCreators()
        {
            return _dbSet
                .Include(x => x.LoadUserDataCreator)
                .ToList();
        }

        public IEnumerable<LoadVolumeTestingData> GetAllWithVolumeMetrics()
        {
            return _dbSet
                .Include(x => x.VolumeMetrics)
                .ToList();
        }

        public void LinkMetric(int loadVolumeMetricId, int metriclId)
        {
            var metric = _webDbContext.Metrics.First(x => x.Id == metriclId);
            var volumeMetric = _dbSet.First(x => x.Id == loadVolumeMetricId);

            volumeMetric.VolumeMetrics.Add(metric);

            _webDbContext.SaveChanges();
        }
    }
}
