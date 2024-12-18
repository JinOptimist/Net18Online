using Everything.Data.Repositories;
using Everything.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;
using WebPortalEverthing.Services.LoadTesting;

namespace WebPortalEverthing.Controllers.LoadTesting
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

        public ApiLoadTestingController(IWebHostEnvironment webHostEnvironment, ILoadTestingRepositoryReal loadTestingRepository, WebDbContext webDbContext, ILoadUserRepositryReal loadUserRepositryReal, LoadAuthService loadAuthService, ILoadVolumeTestingRepositoryReal loadVolumeTestingRepositoryReal)
        {
            _webHostEnvironment = webHostEnvironment;
            _loadTestingRepository = loadTestingRepository;
            _webDbContext = webDbContext;
            _loadUserRepositryReal = loadUserRepositryReal;
            _loadAuthService = loadAuthService;
            _loadVolumeTestingRepositoryReal = loadVolumeTestingRepositoryReal;
        }

        public bool UpdateMetric(MetricCreationViewModel metric)
        {
            Thread.Sleep(1000);
            _loadTestingRepository.UpdateNameByGuid(metric.Guid, metric.Name);
            _loadTestingRepository.UpdateThroughputByGuid(metric.Guid, (decimal)metric.Throughput);
            _loadTestingRepository.UpdateAverageByGuid(metric.Guid, (decimal)metric.Average);

            return true;

        }
    }
}
