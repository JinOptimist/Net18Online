using Everything.Data.Repositories;
using Everything.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;
using WebPortalEverthing.Services.LoadTesting;
using Everything.Data.Interface.Repositories;
using WebPortalEverthing.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebPortalEverthing.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiLoadTestingController : ControllerBase
    {

        private ILoadTestingRepositoryReal _loadTestingRepository;
        private ILoadVolumeTestingRepositoryReal _loadVolumeTestingRepositoryReal;
        private WebDbContext _webDbContext;
        protected const int DEFAULT_METRICS_COUNT = 6;

        private IWebHostEnvironment _webHostEnvironment;

        private ILoadUserRepositryReal _loadUserRepositryReal;
        private LoadAuthService _loadAuthService;
        private LoadVolumeService _loadVolumeService;


        public ApiLoadTestingController(IWebHostEnvironment webHostEnvironment, ILoadTestingRepositoryReal loadTestingRepository, WebDbContext webDbContext, ILoadUserRepositryReal loadUserRepositryReal, LoadAuthService loadAuthService, ILoadVolumeTestingRepositoryReal loadVolumeTestingRepositoryReal, LoadVolumeService loadVolumeService)
        {
            _webHostEnvironment = webHostEnvironment;
            _loadTestingRepository = loadTestingRepository;
            _webDbContext = webDbContext;
            _loadUserRepositryReal = loadUserRepositryReal;
            _loadAuthService = loadAuthService;
            _loadVolumeTestingRepositoryReal = loadVolumeTestingRepositoryReal;
            _loadVolumeService = loadVolumeService;
        }

        public bool UpdateMetric(MetricCreationViewModel metric)
        {
            Thread.Sleep(1000);
            _loadTestingRepository.UpdateNameByGuid(metric.Guid, metric.Name);
            _loadTestingRepository.UpdateThroughputByGuid(metric.Guid, (decimal)metric.Throughput);
            _loadTestingRepository.UpdateAverageByGuid(metric.Guid, (decimal)metric.Average);

            return true;

        }

        public bool RemoveById(int id)
        {
            _loadTestingRepository.Delete(id);
            return true;
        }

        public bool RemoveByGuid(Guid Guid)
        {
            _loadTestingRepository.DeleteByGuid(Guid);
            return true;
        }

        /*   [HttpGet]
           public bool GetLoadVolumes()
           {
               var loadVolumes = _loadVolumeService.GetLoadVolumes();
               return true;
           } нет реализации */

        [HttpPost]
        public MetricCreationViewModel CreateMetric([FromBody] MetricCreationViewModel metric)
        {
            if (!ModelState.IsValid)
            {
                return new MetricCreationViewModel();
            }

            var currentUserId = _loadAuthService.GetUserId();

            // Используем _loadTestingRepository для сохранения данных
            var metricData = new Everything.Data.Models.MetricData
            {
                Name = metric.Name,
                Throughput = (decimal)metric.Throughput,
                Average = (decimal)metric.Average
            };

            _loadTestingRepository.Create(metricData, currentUserId!.Value, metric.LoadVolumeId);

            return metric;
        }

        [Authorize]
        public bool Like(int metricId)
        {
            var userId = _loadAuthService.GetUserId()!.Value;

            return _loadTestingRepository.LikeMetric(metricId, userId);
        }

    }
}
