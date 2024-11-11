using Everything.Data.Interface.Models;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeGirl;
using WebPortalEverthing.Models.LoadTesting;
using WebPortalEverthing.Models.Manga;

namespace WebPortalEverthing.Controllers.LoadVolumeTesting
{
    public class LoadVolumeTestingController : Controller
    {
        private ILoadVolumeTestingRepositoryReal _loadVolumeTestingRepositoryReal;
        private ILoadTestingRepositoryReal _metricRepositoryReal; // ILoadTestingRepositoryReal это репозиторий для метрик

        public LoadVolumeTestingController(ILoadVolumeTestingRepositoryReal loadVolumeTestingRepositoryReal, ILoadTestingRepositoryReal metricRepositoryReal)
        {
            _loadVolumeTestingRepositoryReal = loadVolumeTestingRepositoryReal;
            _metricRepositoryReal = metricRepositoryReal;
        }

        public IActionResult IndexLoadVolumeView()
        {
            var loadVolumeViewModels = _loadVolumeTestingRepositoryReal
                .GetAllWithVolumeMetrics()
                .Select(x => new LoadVolumeShortInfoViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Metrics = x.VolumeMetrics.Select(metricData =>
                        new MetricNameAndIdViewModel
                        {
                            Id = metricData.Id,
                            Name = metricData.Name,
                        }).ToList()
                })
                .ToList();

            var metricViewModels = _metricRepositoryReal
                .GetWithoutVolumeLoad()
                .Select(x => new MetricNameAndIdViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            var viewModel = new IndexVolumeLoadViewModel
            {
                LoadVolumes = loadVolumeViewModels,
                Metrics = metricViewModels
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateloadVolumeView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateloadVolumeView(CreateLoadVolumeModel viewModel)
        {
            var loadVolumeData = new LoadVolumeTestingData
            {
                Title = viewModel.Title,
                Description = viewModel.Description
            };

            _loadVolumeTestingRepositoryReal.Add(loadVolumeData);

            return RedirectToAction("IndexLoadVolumeView");
        }

        [HttpPost]
        public IActionResult LinkLoadVolumeAndMetric(int loadVolumeId, int metricId)
        {
            _loadVolumeTestingRepositoryReal.LinkMetric(loadVolumeId, metricId);
            return RedirectToAction("IndexLoadVolumeView");
        }
    }
}
