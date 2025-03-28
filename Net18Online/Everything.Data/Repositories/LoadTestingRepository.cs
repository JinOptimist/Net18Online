﻿using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface ILoadTestingRepositoryReal : ILoadTestingRepository<MetricData>
    {
        IEnumerable<MetricData> GetWithoutVolumeLoad();
        IEnumerable<MetricData> GetAllWithCreatorsAndLoadVolume();
        IEnumerable<MetricData> GetAllByAuthorId(int userId);
        bool HasSimilarName(string name);
        bool IsNameUniq(string name);
        void Create(MetricData metricData, int currentUserId, int LoadVolumeId);
        /// <summary>
        /// Return true if metric wasn't like. And now she is
        /// Return false if metric was liked but not is not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        bool LikeMetric(int metricId, int userId);
    }
    public class LoadTestingRepository : BaseRepository<MetricData>, ILoadTestingRepositoryReal
    {
        //     private WebDbContext _webDbContext; у родителя теперь

        public LoadTestingRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void Create(MetricData metricData, int currentUserId, int LoadVolumeId)
        {
            var creator = _webDbContext.LoadUsers.First(x => x.Id == currentUserId);
            var LoadVolume = _webDbContext.LoadVolumeTestingMetrics.First(x => x.Id == LoadVolumeId);

            metricData.LoadUserDataCreator = creator;
            metricData.LoadVolumeTesting = LoadVolume;

            Add(metricData);
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

        public IEnumerable<MetricData> GetAllWithCreatorsAndLoadVolume()
        {
            return _dbSet
                .Include(x => x.LoadUserDataCreator)
                .Include(x => x.LoadVolumeTesting)
                .Include(x => x.UserWhoLikeIt)
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

        public IEnumerable<MetricData> GetAllByAuthorId(int userId)
        {

            return _dbSet
                .Where(x => x.LoadUserDataCreator != null
                    && x.LoadUserDataCreator.Id == userId)
                .ToList();
        }

        public bool LikeMetric(int metricId, int userId)
        {
            var metric = _dbSet
                .Include(x => x.UserWhoLikeIt)//для высоконагруженных приложений не подходит,
                                              //нужна модель с двумя полями,
                                              //id метрики и id юзера , который лайкнул
                .First(x => x.Id == metricId);

            var user = _webDbContext.LoadUsers.First(x => x.Id == userId);

            var isUserAlreadyLikeTheMetric = metric
                .UserWhoLikeIt
                .Any(u => u.Id == userId);

            if (isUserAlreadyLikeTheMetric)
            {
                metric.UserWhoLikeIt.Remove(user);
                _webDbContext.SaveChanges();
                return false;
            }

            metric.UserWhoLikeIt.Add(user);
            _webDbContext.SaveChanges();

            return true;
        }
    }
}
