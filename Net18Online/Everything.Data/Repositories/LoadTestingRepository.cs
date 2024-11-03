﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface ILoadTestingRepositoryReal : ILoadTestingRepository<MetricData>
    {
    }
    public class LoadTestingRepository : ILoadTestingRepositoryReal
    {
        private WebDbContext _webDbContext;

        public LoadTestingRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public void Add(MetricData data)
        {
            _webDbContext.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.Metrics.Any();
        }

        public void Delete(MetricData data)
        {
            _webDbContext.Metrics.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            Delete(data);
        }

        public void Delete(Guid Guid)
        {
            var data = Get(Guid);
            Delete(data);
        }

        public MetricData? Get(int id)
        {
            return _webDbContext.Metrics.FirstOrDefault(x => x.Id == id);
        }
        public MetricData? Get(Guid Guid)
        {
            return _webDbContext.Metrics.FirstOrDefault(x => x.Guid == Guid);
        }

        public IEnumerable<MetricData> GetAll()
        {
            return GetFinilizeMetric().ToList();
        }

        public IEnumerable<MetricData> GetMostPopular()
        {
            return GetFinilizeMetric()
                .Take(3)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public void UpdateThroughput(Guid Guid, decimal Throughput)
        {
            var Metric = _webDbContext.Metrics.First(x => x.Guid == Guid);

            Metric.Throughput = Throughput;

            _webDbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var Metric = _webDbContext.Metrics.First(x => x.Id == id);

            Metric.Name = newName;

            _webDbContext.SaveChanges();
        }

        private IQueryable<MetricData> GetFinilizeMetric()
        {
            return _webDbContext
                .Metrics
                .Where(x => !string.IsNullOrEmpty(x.Name));
        }
    }
}
